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
using Microsoft.AspNetCore.HostFiltering;


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
           services.Configure<CookiePolicyOptions>(opts => {
               opts.CheckConsentNeeded = context => true;
           });

           services.AddDistributedMemoryCache();

           services.AddSession(options => {
               options.IdleTimeout = TimeSpan.FromMinutes(30);
               options.Cookie.IsEssential = true;
           });

           services.AddHsts(opts => {
               opts.MaxAge = TimeSpan.FromDays(1);
               opts.IncludeSubDomains = true;
           });

           services.Configure<HostFilteringOptions>(opts => {
               opts.AllowedHosts.Clear();
               opts.AllowedHosts.Add("*.example.com");
           })
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) {
            // app.UseDeveloperExceptionPage(); 
            app.UseExceptionHandler("/error.html");
            if (env.IsProduction()) {
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStatusCodePages("text/html", Responses.DefaultResponse);
            app.UseCookiePolicy();
            app.UseMiddleware<ConsentMiddleware>();
            app.UseSession();
            // app.UseRouting();

            // app.Use(async (context, next) => {
            //     await context.Response.WriteAsync($"HTTPS Request: {context.Request.IsHttps} \n");
            //     await next();
            // });

            app.Use(async (context, next) => {
                if (context.Request.Path == "/error") {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await Task.CompletedTask;
                } else {
                    await next();
                }
            });

            app.Run(context => {
                throw new Exception("Shomething has gone wrong");
            });

            // app.UseEndpoints(endpoints => {
            //     endpoints.MapGet("/cookie", async context => {
                    // int counter1 = 
                    //     int.Parse(context.Request.Cookies["counter1"] ?? "0") + 1;
                    // context.Response.Cookies.Append("counter1", counter1.ToString(),
                    //     new CookieOptions {
                    //         MaxAge = TimeSpan.FromMinutes(30),
                    //         IsEssential = true
                    //     });
                    // int counter2 =
                    //     int.Parse(context.Request.Cookies["counter2"] ?? "0") + 1;
                    // context.Response.Cookies.Append("counter2", counter1.ToString(),
                    //     new CookieOptions {
                    //         MaxAge = TimeSpan.FromMinutes(30)
                    //     });
                    // await context.Response
                    //     .WriteAsync($"Counter1: {counter1}, Counter 2: {counter2}");

                //     int counter1 = (context.Session.GetInt32("counter1") ?? 0) + 1;
                //     int counter2 = (context.Session.GetInt32("counter2") ?? 0) + 1;
                //     context.Session.SetInt32("counter1", counter1);
                //     context.Session.SetInt32("counter2", counter2);
                //     await context.Session.CommitAsync();
                //     await context.Response
                //         .WriteAsync($"Counter1: {counter1}, Counter2: {counter2}");
                // });

                // endpoints.MapGet("clear", context => {
                //     context.Response.Cookies.Delete("counter1");
                //     context.Response.Cookies.Delete("counter2");
                //     context.Response.Redirect("/");
                //     return Task.CompletedTask;
                // });

                // endpoints.MapFallback(async context =>
                //     await context.Response.WriteAsync("Hello World!"));
            // });
        }
    }
}
