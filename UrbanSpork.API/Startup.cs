using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.Domain.ReadModel.QueryHandlers;
using UrbanSpork.Domain.SLCQRS.ReadModel;

namespace UrbanSpork.API
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

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

            //builder.RegisterType<QueryProcessor>().As<IQueryProcessor>().InstancePerLifetimeScope();
            //builder.RegisterType<GetAllUsersQueryHandler>().As<IQueryHandler<GetAllUsersQuery, string>>().InstancePerLifetimeScope();
            //builder.RegisterType<GetAllUsersQuery>().As<IQuery<string>>().InstancePerLifetimeScope();


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
