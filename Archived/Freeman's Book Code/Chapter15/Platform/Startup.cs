using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;

namespace Platform
{
    public class Startup
    {
        public Startup(IConfiguration configService) {
            Configuration = configService;
        }

        private IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services) {
            // configuration data can be accessed here
            services.Configure<MessageOptions>(Configuration.GetSection("Location"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider($"{env.ContentRootPath}/staticfiles"),
                RequestPath = "/files"
            });

            app.UseRouting();

            app.UseMiddleware<LocationMiddleware>();
            
            // app.Use(async (context, next) => {
            //     string defaultDebug = Configuration["Logging:LogLevel:Default"];
            //     await context.Response
            //         .WriteAsync($"The config setting is: {defaultDebug}");
            //     string environ = Configuration["ASPNETCORE_ENVIRONMENT"];
            //     await context.Response
            //         .WriteAsync($"\nThe env setting is: {environ}");
            //     string wsID = Configuration["WebService:Id"];
            //     string wsKey = Configuration["WebService:Key"];
            //     await context.Response.WriteAsync($"\nThe secret ID is: {wsID}");
            //     await context.Response.WriteAsync($"\nThe secret Key is: {wsKey}");
            // });

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    logger.LogDebug("Response for / started");
                    await context.Response.WriteAsync("Hello World!");
                    logger.LogDebug("Response for / completed");
                });
            });
        }
    }
}
