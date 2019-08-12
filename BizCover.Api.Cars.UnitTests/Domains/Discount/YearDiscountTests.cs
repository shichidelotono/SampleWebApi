﻿using BizCover.Api.Cars.AppSettings;
using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Domains.Discount;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BizCover.Api.Cars.UnitTests.Domains.Discount
{
    public class YearDiscountTests
    {
        private Mock<IOptions<DiscountSetting>> _mockDiscountSetting;
        private YearDiscount _yearDiscount;

        public YearDiscountTests()
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

            _yearDiscount = new YearDiscount(_mockDiscountSetting.Object);
        }

        [Fact]
        public void given_no_car_year_less_than_eligible_year_for_discount_should_not_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009)
            };

            // act
            var result = _yearDiscount.Apply(givenCars, null);

            // assert
            Assert.Equal(20m, result);
        }

        [Fact]
        public void given_no_car_year_less_than_eligible_year_for_discount_with_already_discounted_total_cost_should_not_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2009)
            };

            // act
            var result = _yearDiscount.Apply(givenCars, 15);

            // assert
            Assert.Equal(20m, result);
        }

        [Fact]
        public void given_car_year_less_than_eligible_year_for_discount_should_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 1999),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 1989)
            };

            // act
            var result = _yearDiscount.Apply(givenCars, null);

            // assert
            Assert.Equal(10m, result);
        }

        [Fact]
        public void given_car_year_less_than_eligible_year_for_discount_with_already_discounted_total_cost_should_not_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 1999),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 1999)
            };

            // act
            var result = _yearDiscount.Apply(givenCars, 10);

            // assert
            Assert.Equal(10m, result);
        }

        [Fact]
        public void given_some_car_year_less_than_eligible_year_for_discount_with_already_discounted_total_cost_should_not_have_discount()
        {
            // setup
            var givenCars = new List<CarDomain>
            {
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 2019),
                new CarDomain(1, "silver", "japan", "subaru", "outback", 10m, 1999)
            };

            // act
            var result = _yearDiscount.Apply(givenCars, null);

            // assert
            Assert.Equal(15m, result);
        }
    }
}
