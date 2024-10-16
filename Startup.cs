using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Areas.Customers.SignalR;

namespace Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(Configuration);
            services.AddIdentityServices(Configuration);

            services.AddSession();
            services.AddRazorPages();
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthSection = Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthSection["ClientId"];
                    options.ClientSecret = googleAuthSection["ClientSecret"];
                })
                .AddFacebook(FacebookOption =>
                {
                    FacebookOption.AppId = Configuration["Authentication:Facebook:AppId"];
                    FacebookOption.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapHub<MessageHub>("hubs/messages");
                endpoints.MapRazorPages();
            });
        }
    }
}
