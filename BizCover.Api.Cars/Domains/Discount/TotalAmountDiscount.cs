using BizCover.Api.Cars.AppSettings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace BizCover.Api.Cars.Domains.Discount
{
    public class TotalAmountDiscount : ITotalAmountDiscount
    {
        private readonly IOptions<DiscountSetting> _discountSetting;

        public TotalAmountDiscount(IOptions<DiscountSetting> discountSetting)
        {
            _discountSetting = discountSetting;
            ApplyOrder = _discountSetting.Value.EligibleTotalCostDiscountApplyOrder;
        }

        public decimal Apply(List<CarDomain> cars, decimal? discountedTotalCost)
        {
            var totalCost = cars.Sum(c => c.Price);

            if (totalCost > _discountSetting.Value.EligibleTotalCost)
            {
                if (discountedTotalCost != null)
                    return discountedTotalCost.Value * (1 - _discountSetting.Value.EligibleTotalCostDiscount);
                else
                    return totalCost * (1 - _discountSetting.Value.EligibleTotalCostDiscount);
            }

            return discountedTotalCost != null ? discountedTotalCost.Value : totalCost;
        }

        public int ApplyOrder { get; }
    }
}
