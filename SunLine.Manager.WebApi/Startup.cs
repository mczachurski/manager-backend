using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.WebApi.DependencyInjection;
using System;

namespace SunLine.Manager.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        
        // Entry point for the application.
        //public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<DatabaseContext>();
            
            services.AddMvc();
            
            var embeddedModule = new EmbeddedModule();
            embeddedModule.Load(services);
            LogRegistrations(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseIISPlatformHandler();
            
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
