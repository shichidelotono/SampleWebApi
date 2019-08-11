using System.Collections.Generic;

namespace BizCover.Api.Cars.Domains.Discount
{
    public interface IDiscount
    {
        decimal Apply(List<CarDomain> cars, decimal? discountedTotalCost);
        int ApplyOrder { get; }
    }
}
