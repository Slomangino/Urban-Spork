﻿using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.Domain.ReadModel.QueryHandlers;
using UrbanSpork.Domain.ReadModel.Repositories;
using UrbanSpork.Domain.SLCQRS.ReadModel;
using UrbanSpork.Domain.SLCQRS.WriteModel;
using UrbanSpork.Domain.WriteModel;
using UrbanSpork.Domain.WriteModel.Commands;

namespace UrbanSpork.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            
            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; private set; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the ConfigureContainer method, below.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection. Don't build or return
            // any IServiceProvider or the ConfigureContainer method
            // won't get called.
            //services.AddAutofac();
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
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<BaseRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();


            //commands
            builder.RegisterType<CommandDispatcher>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
            builder.RegisterType<CreateSingleUserCommandHandler>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
            builder.RegisterType<CreateSingleUserCommand>()
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
            //good
            builder.RegisterType<QueryProcessor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetAllUsersQueryHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetAllUsersQuery>().AsImplementedInterfaces().InstancePerLifetimeScope();

        }

        // Configure is where you add middleware. This is called after
        // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }

    }
}
