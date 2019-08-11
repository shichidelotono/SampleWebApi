using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Domains.Discount;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Services
{
    public class PriceService : IPriceService
    {
        private readonly ICarsService _carsService;

        public PriceService(ICarsService carsService, IServiceProvider serviceProvider)
        {
            _carsService = carsService;

            var services = serviceProvider.GetServices<IDiscount>();

            YearDiscount = services.First(o => o.GetType() == typeof(YearDiscount));
            NumberOfCarsDiscount = services.First(o => o.GetType() == typeof(NumberOfCarsDiscount));
            TotalAmountDiscount = services.First(o => o.GetType() == typeof(TotalAmountDiscount));
        }

        public async Task<decimal> CalculateSalePrice(IEnumerable<int> carIds)
        {
            var cars = new List<CarDomain>();

            foreach (var id in carIds)
                cars.Add(await _carsService.Get(id));

            var price = YearDiscount.Apply(cars, null);
            price = NumberOfCarsDiscount.Apply(cars, price);
            price = TotalAmountDiscount.Apply(cars, price);

            return price;
        }

        public IDiscount YearDiscount { get; set; }

        public IDiscount NumberOfCarsDiscount { get; set; }

        public IDiscount TotalAmountDiscount { get; set; }
    }
}
