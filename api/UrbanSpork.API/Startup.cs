using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using UrbanSpork.CQRS.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.ReadModel;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.ReadModel.QueryHandlers;
using UrbanSpork.WriteModel;
using UrbanSpork.WriteModel.CommandHandlers;
using UrbanSpork.WriteModel.Commands;
using UrbanSpork.WriteModel.WriteModel.Commands;

namespace UrbanSpork.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            //this is the builder object
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            
            Configuration = builder.Build();

            Mapper.Initialize(cfg => {
                cfg.CreateMap<UserAggregate, UserDTO>();
                cfg.CreateMap<UserDTO, UserAggregate>();
                cfg.CreateMap<UserDetailProjection, UserDetailProjectionDTO>()
                    .ForMember(dest => dest.PermissionList,
                        opt => opt.MapFrom(src =>
                            JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                                src.PermissionList)))
                    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
                cfg.CreateMap<UserDetailProjection, LoginUserDTO>()
                    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
                cfg.CreateMap<UserDetailProjection, OffBoardUserDTO>()
                    .ForMember(dest => dest.PermissionList,
                        opt => opt.MapFrom(src =>
                            JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                                src.PermissionList)));
                cfg.CreateMap<UserAggregate, UpdateUserInformationDTO>()
                    .ForMember(dest => dest.ForID, opt => opt.MapFrom(src => src.Id));
                cfg.CreateMap<PermissionAggregate, PermissionDTO>();
                cfg.CreateMap<PermissionDTO, PermissionAggregate>();
                cfg.CreateMap<PermissionDTO, PermissionDetailProjection>();
                cfg.CreateMap<PermissionDetailProjection, PermissionDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PermissionId));
                cfg.CreateMap<UserManagementProjection, UserManagementDTO>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Position))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
                cfg.CreateMap<SystemActivityProjection, SystemActivityDTO>();
            });
        }

        public IConfiguration Configuration { get; private set; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the ConfigureContainer method, below.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection. Don't build or return
            // any IServiceProvider or the ConfigureContainer method
            // won't get called.
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            
            services.AddEntityFrameworkNpgsql().AddDbContext<UrbanDbContext>(
                options => options.UseNpgsql(connectionString, m => m.MigrationsAssembly("UrbanSpork.DataAccess")), ServiceLifetime.Transient);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                      );
            });
            services.AddMvc();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Utility
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PermissionRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<QueryProcessor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CommandDispatcher>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserDTO>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserAggregate>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PermissionManager>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PermissionAggregate>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Session>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Repository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EventStore>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GenericEventPublisher>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UrbanDbContext>().AsImplementedInterfaces().InstancePerLifetimeScope();



            // Projections
            builder.RegisterType<UserDetailProjection>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PermissionDetailProjection>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PendingRequestsProjection>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserManagementProjection>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SystemDropdownProjection>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ApproverActivityProjection>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SystemActivityProjection>().AsSelf().InstancePerLifetimeScope();


            //Commands
            //users
            builder.RegisterType<CreateSingleUserCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UpdateSingleUserCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DisableSingleUserCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EnableSingleUserCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserPermissionsRequestedCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DenyUserPermissionRequestCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GrantUserPermissionCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RevokeUserPermissionCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //permissions
            builder.RegisterType<CreatePermissionCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UpdatePermissionInfoCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DisablePermissionCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EnablePermissionCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();

            
            //Command Handlers
            //users
            builder.RegisterType<CreateSingleUserCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UpdateSingleUserCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DisableSingleUserCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EnableSingleUserCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserPermissionsRequestedCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DenyUserPermissionRequestCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GrantUserPermissionCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RevokeUserPermissionCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //permissions
            builder.RegisterType<CreatePermissionCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UpdatePermissionInfoCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DisablePermissionCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EnablePermissionCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();



            //Query
            builder.RegisterType<GetUserDetailByIdQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetUserCollectionQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetUserManagementProjectionQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetPermissionByIdQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetAllPermissionsQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetSystemDropDownProjectionQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetApproverActicityProjectionQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetPendingRequestsProjectionQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetSystemActivityReportQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetOffboardUserPermissionsQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetLoginUsersQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();


            //Query Handlers
            builder.RegisterType<GetUserDetailByIdQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetPermissionByIdQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetAllPermissionsQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetUserManagementProjectionQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetUserCollectionQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetSystemDropDownProjectionQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetApproverActivityProjectionQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetPendingRequestsProjectionQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetSystemActivityReportQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetOffboardUserPermissionsQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetLoginUsersQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();

        }

        // Configure is where you add middleware. This is called after
        // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("CorsPolicy");
            
            app.UseMvc();
        }
    }
}
