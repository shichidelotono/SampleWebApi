using BizCover.Api.Cars.AppSettings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace BizCover.Api.Cars.Domains.Discount
{
    public class YearDiscount : IDiscount
    {
        private readonly IOptions<DiscountSetting> _discountSetting;

        public YearDiscount(IOptions<DiscountSetting> discountSetting)
        {
            _discountSetting = discountSetting;
            ApplyOrder = _discountSetting.Value.EligibleYearDiscountApplyOrder;
        }

        public decimal Apply(List<CarDomain> cars, decimal? discountedTotalCost)
        {
            var totalAfterEligibleYear = cars
                .Where(c => c.Year >= _discountSetting.Value.EligibleYear)
                .Sum(c => c.Price);

            var totalBeforeEligibleYear = cars
                .Where(c => c.Year < _discountSetting.Value.EligibleYear)
                .Sum(c => c.Price * (1 - _discountSetting.Value.EligibleYearDiscount));

            return totalAfterEligibleYear + totalBeforeEligibleYear;
        }

        public int ApplyOrder { get; }
    }
}
