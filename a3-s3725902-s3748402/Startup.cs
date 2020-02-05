using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NwbaSystem.Data;
using NwbaSystem.HostedService;

namespace NwbaSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<NwbaContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(NwbaContext)));

                // Enable lazy loading.
                options.UseLazyLoadingProxies();
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Make the session cookie essential.
                options.Cookie.IsEssential = true; // Make the session cookie essential
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = TimeSpan.FromSeconds(10); // session will expire after 30 seconds and prompt another login
            });

            services.AddControllersWithViews();
            // services.AddHostedService<TimedHostedService>();
            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            //middleware set the default authentication scheme for the app
            
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth",config =>
                {
                    config.Cookie.Name = "User.Cookie";
                    config.SlidingExpiration = false;
                    config.ExpireTimeSpan = TimeSpan.FromSeconds(10);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
           
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days.
                // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication(); //set the HttpContext.User property and run Authorization Middleware for requests
            app.UseAuthorization(); //set the HttpContext.User property and run Authorization Middleware for requests

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseStatusCodePages(
    "text/html", "<html><h1>are you lost son?</h1></html>");
        }
        
        
    }
}
