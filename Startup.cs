using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using my_app_chat.Hubs;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace my_app_chat
{
    public class Startup
    {   
        // readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddCors(options =>
            // {
            //     options.AddPolicy(MyAllowSpecificOrigins,
            //     builder =>
            //     {
            //         builder.WithOrigins("http://localhost:5000/");
            //     });
            // });
           

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:5000/ChatHub")
                     .SetIsOriginAllowed((host) => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
        services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           // app.UseCors(MyAllowSpecificOrigins); 
            app.UseRouting();
            app.UseCors("CorsPolicy");
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/ChatHub");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            app.UseSpa(spa =>  
            {  
                // To learn more about options for serving an Angular SPA from ASP.NET Core,  
                // see https://go.microsoft.com/fwlink/?linkid=864501  
  
                spa.Options.SourcePath = "ClientApp";  
  
                if (env.IsDevelopment())  
                {  
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");  
                    spa.UseAngularCliServer(npmScript: "start");  
                }  
            });  
        }
    }
}
