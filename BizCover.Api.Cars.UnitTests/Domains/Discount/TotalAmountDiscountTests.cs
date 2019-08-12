using BizCover.Api.Cars.AppSettings;
using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Domains.Discount;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BizCover.Api.Cars.UnitTests.Domains.Discount
{
    public class TotalAmountDiscountTests
    {
        private Mock<IOptions<DiscountSetting>> _mockDiscountSetting;
        private TotalAmountDiscount _totalAmountDiscount;

        public TotalAmountDiscountTests()
        {
            _mockDiscountSetting = new Mock<IOptions<DiscountSetting>>();

            var _discountSettingStub = new DiscountSetting
            {
                EligibleYear = 2000,
                EligibleYearDiscount = 0.5m,
                EligibleYearDiscountApplyOrder = 1,
                EligibleNumberOfCars = 2,
                EligibleNumberOfCarsDiscount = 0.5m,
                EligibleNumberOfCarsDiscountApplyOrder = 2,
                EligibleTotalCost = 25,
                EligibleTotalCostDiscount = 0.5m,
                EligibleTotalCostDiscountApplyOrder = 3,
            };

            _mockDiscountSetting.Setup(q => q.Value).Returns(_discountSettingStub);

            _totalAmountDiscount = new TotalAmountDiscount(_mockDiscountSetting.Object);
        }

        [Fact]
        public void given_less_than_total_eligible_cost_should_not_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009)
            };

            // act
            var result = _totalAmountDiscount.Apply(givenCars, null);

            // assert
            Assert.Equal(20m, result);
        }

        [Fact]
        public void given_less_than_total_eligible_cost_with_already_discounted_total_cost_should_not_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009)
            };

            // act
            var result = _totalAmountDiscount.Apply(givenCars, 15);

            // assert
            Assert.Equal(15m, result);
        }

        [Fact]
        public void given_more_than_total_eligible_cost_should_not_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 30m, 2009)
            };

            // act
            var result = _totalAmountDiscount.Apply(givenCars, null);

            // assert
            Assert.Equal(20m, result);
        }

        [Fact]
        public void given_more_than_total_eligible_cost_with_already_discounted_total_cost_should_not_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 30m, 2009)
            };

            // act
            var result = _totalAmountDiscount.Apply(givenCars, 30);

            // assert
            Assert.Equal(15m, result);
        }
    }
}
