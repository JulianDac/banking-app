using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Http;
using NwbaApi.Data;
using NwbaApi.Models.DataManager;
using System.Text.Json;

namespace NwbaApi
{
    public class Startup
    {
        private const string _enableCrossOriginRequestsKey = "EnableCrossOriginRequests";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy(_enableCrossOriginRequestsKey, builder => builder.AllowAnyOrigin()));
           
            services.AddDbContext<NwbaContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("NwbaContext")));

            services.AddTransient<CustomerManager>();
            services.AddTransient<AddressManager>();
            services.AddTransient<BillPayManager>();
            services.AddTransient<LoginManager>();
            services.AddTransient<TransactionManager>();
            services.AddTransient<AccountManager>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_enableCrossOriginRequestsKey);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
