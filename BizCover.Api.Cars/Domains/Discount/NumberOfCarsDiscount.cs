using BizCover.Api.Cars.AppSettings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace BizCover.Api.Cars.Domains.Discount
{
    public class NumberOfCarsDiscount : IDiscount
    {
        private readonly IOptions<DiscountSetting> _discountSetting;

        public NumberOfCarsDiscount(IOptions<DiscountSetting> discountSetting)
        {
            _discountSetting = discountSetting;
            ApplyOrder = _discountSetting.Value.EligibleNumberOfCarsDiscountApplyOrder;
        }

        public decimal Apply(List<CarDomain> cars, decimal? discountedTotalCost)
        {
            var totalCost = cars.Sum(c => c.Price);

            if (cars.Count > _discountSetting.Value.EligibleNumberOfCars)
            {
                if (discountedTotalCost != null)
                    return discountedTotalCost.Value * (1 - _discountSetting.Value.EligibleNumberOfCarsDiscount);
                else
                    return totalCost * (1 - _discountSetting.Value.EligibleNumberOfCarsDiscount);
            }

            return discountedTotalCost != null ? discountedTotalCost.Value : totalCost;
        }

        public int ApplyOrder { get; }
    }
}
