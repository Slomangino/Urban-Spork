using Autofac;
using AutoMapper;
using UrbanSpork.CQRS.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrbanSpork.Common.DataTransferObjects;
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
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<UserDTO, UserInputDTO>();
                cfg.CreateMap<UserInputDTO, UserDTO>();
                cfg.CreateMap<UserDetailProjection, UserDTO>();
                cfg.CreateMap<UserDTO, UserDetailProjection>();
                // cfg.CreateMap<Bar, BarDto>();
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
            services.AddEntityFrameworkNpgsql().AddDbContext<UrbanDbContext>
                (options => options.UseNpgsql(connectionString, m => m.MigrationsAssembly
                ("UrbanSpork.DataAccess")));

            services.AddMvc();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Utility
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<QueryProcessor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CommandDispatcher>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserDTO>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserAggregate>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Session>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Repository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EventStore>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GenericEventPublisher>().AsImplementedInterfaces().InstancePerLifetimeScope();

            // Projections
            builder.RegisterType<UserDetailProjection>().AsSelf().InstancePerLifetimeScope();


            //Commands
            builder.RegisterType<CreateSingleUserCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UpdateSingleUserCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();


            //Command Handlers
            builder.RegisterType<CreateSingleUserCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UpdateSingleUserCommandHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();


            //Query
            builder.RegisterType<GetAllUsersQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetUserByIdQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetUserCollectionQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //Query Handlers
            builder.RegisterType<GetUserByIdQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetAllUsersQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetUserCollectionQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
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
            app.UseMvc();
        }
    }
}
