﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using NwbaAdmin.Data;

namespace NwbaAdmin
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
            services.AddControllersWithViews();

            services.AddDbContext<NwbaAdminContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("NwbaAdminContext")));

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Make the session cookie essential.
                options.Cookie.IsEssential = true; // Make the session cookie essential
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = TimeSpan.FromSeconds(10); // session will expire after 30 seconds and prompt another login
            });


            services.AddAuthentication("CookieAuth")
              .AddCookie("CookieAuth", config =>
              {
                  config.Cookie.Name = "User.Cookie";
                  config.SlidingExpiration = false;
                  config.ExpireTimeSpan = TimeSpan.FromSeconds(10);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });

            app.UseStatusCodePages(
            "text/html", "<html><h1>are you lost son?</h1></html>");
        }

    }
}

