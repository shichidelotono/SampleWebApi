using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Domains.Discount;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Services
{
    public class PriceService : IPriceService
    {
        private readonly ICarsService _carsService;
        private IYearDiscount _yearDiscount;
        private INumberOfCarsDiscount _numberOfCarsDiscount;
        private ITotalAmountDiscount _totalAmountDiscount;

        public PriceService(ICarsService carsService, IYearDiscount yearDiscount, INumberOfCarsDiscount numberOfCarsDiscount, ITotalAmountDiscount totalAmountDiscount)
        {
            _carsService = carsService;
            _yearDiscount = yearDiscount;
            _numberOfCarsDiscount = numberOfCarsDiscount;
            _totalAmountDiscount = totalAmountDiscount;
        }

        public async Task<decimal> CalculateSalePrice(IEnumerable<int> carIds)
        {
            var cars = new List<CarDomain>();

            foreach (var id in carIds)
                cars.Add(await _carsService.Get(id));

            var price = _yearDiscount.Apply(cars, null);
            price = _numberOfCarsDiscount.Apply(cars, price);
            price = _totalAmountDiscount.Apply(cars, price);

            return price;
        }
    }
}
