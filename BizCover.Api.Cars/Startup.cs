using BizCover.Api.Cars.AppSettings;
using BizCover.Api.Cars.Domains.Discount;
using BizCover.Api.Cars.Repository;
using BizCover.Api.Cars.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BizCover.Api.Cars
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
            services.Configure<DiscountSetting>(Configuration.GetSection("DiscountSetting"));
            services.AddSingleton<IDiscount, YearDiscount>();
            services.AddSingleton<IDiscount, NumberOfCarsDiscount>();
            services.AddSingleton<IDiscount, TotalAmountDiscount>();
            services.AddSingleton<BizCover.Repository.Cars.ICarRepository, BizCover.Repository.Cars.CarRepository>();
            services.AddSingleton<ICarsRepository, CarsRepository>();
            services.AddSingleton<ICarsService, CarsService>();
            services.AddSingleton<IPriceService, PriceService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
