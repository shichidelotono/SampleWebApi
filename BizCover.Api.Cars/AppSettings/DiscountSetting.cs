namespace BizCover.Api.Cars.AppSettings
{
    public class DiscountSetting
    {
        public int EligibleYear { get; set; }
        public decimal EligibleYearDiscount { get; set; }
        public int EligibleYearDiscountApplyOrder { get; set; }
        public int EligibleNumberOfCars { get; set; }
        public decimal EligibleNumberOfCarsDiscount { get; set; }
        public int EligibleNumberOfCarsDiscountApplyOrder { get; set; }
        public int EligibleTotalCost { get; set; }
        public decimal EligibleTotalCostDiscount { get; set; }
        public int EligibleTotalCostDiscountApplyOrder { get; set; }
    }
}
