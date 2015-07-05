using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Logging;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.WebApi.DependencyInjection;
using System;

namespace SunLine.Manager.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configurationBuilder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
                
            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                configurationBuilder.AddUserSecrets();
            }
            
            configurationBuilder.AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddEntityFramework()
                .AddInMemoryStore()
                .AddDbContext<DatabaseContext>();
            
            services.AddMvc();
            
            var embeddedModule = new EmbeddedModule();
            embeddedModule.Load(services);
            
            LogRegistrations(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Verbose;
            loggerFactory.AddConsole();
            
            if (env.IsDevelopment())
            {
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else
            {
                // WebApi error handler?
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            
            app.UseMvc();
        }
        
        private void LogRegistrations(IServiceCollection services)
        {            
            foreach(var service in services)
            {
                Console.WriteLine($"IT: {service.ImplementationType} - ST: {service.ServiceType}");
            }
        }
    }
}
