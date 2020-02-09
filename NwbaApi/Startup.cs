using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NwbaApi.Data;
using NwbaApi.Models;

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

            services.AddTransient<CustomerRepository>();
            services.AddTransient<AddressRepository>();
            services.AddTransient<BillPayRepository>();
            services.AddTransient<LoginRepository>();
            services.AddTransient<TransactionRepository>();
            services.AddTransient<AccountRepository>();

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
