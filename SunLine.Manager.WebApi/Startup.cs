using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Configuration;
using Autofac;
using Autofac.Dnx;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        
        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
                
            if (env.IsEnvironment("Development"))
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                configurationBuilder.AddUserSecrets();
            }
            
            configurationBuilder.AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddSqlServer().AddDbContext<DatabaseContext>();
            
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            
            services.AddMvc();
            
            IServiceProvider serviceProvider = ConfigureDependencyInjection(services);
            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsEnvironment("Development"))
            {
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            
            app.UseMvc();
        }
        
        public IServiceProvider ConfigureDependencyInjection(IServiceCollection services)  
        {            
            var builder = new ContainerBuilder();
            builder.RegisterModule(new InjectionModule());
            builder.Populate(services);
           
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }
    }
}
