using BizCover.Api.Cars.Controllers;
using BizCover.Api.Cars.Models;
using BizCover.Api.Cars.Repository;
using BizCover.Api.Cars.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace BizCover.Api.Cars.IntegrationTests
{
    public class UpdateCarTest
    {
        private BizCover.Repository.Cars.CarRepository _bizCoverCarRepository;
        private CarsRepository _carsRepository;
        private CarsService _carsService;
        private CarsController _carsController;

        public UpdateCarTest()
        {
            _bizCoverCarRepository = new BizCover.Repository.Cars.CarRepository();
            _carsRepository = new CarsRepository(_bizCoverCarRepository);
            _carsService = new CarsService(_carsRepository);
            _carsController = new CarsController(_carsService);
        }

        [Fact]
        public async Task insert_car_should_add_car_to_repository()
        {
            // setup
            var givenId = 1;
            var givenRequest = new UpdateCarRequest
            {
                Colour = "silver",
                CountryManufactured = "japan",
                Make = "subaru",
                Model = "outback",
                Price = 8000m,
                Year = 2009
            };
            var carBeforeUpdate = await _carsService.Get(givenId);

            // act
            var result = await _carsController.Put(1, givenRequest);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as AddCarResponse;

            // verify 
            var carAfterUpdate = await _carsService.Get(givenId);
            Assert.Equal(200, okResult.StatusCode.Value);
            Assert.True(response.IsSuccess);
            Assert.Empty(response.ErrorMessage);
            Assert.Equal(carBeforeUpdate.Id, carAfterUpdate.Id);
            Assert.NotEqual(carBeforeUpdate.Colour, carAfterUpdate.Colour);
            //Assert.NotEqual(carBeforeUpdate.CountryManufactured, carAfterUpdate.CountryManufactured);
            Assert.NotEqual(carBeforeUpdate.Make, carAfterUpdate.Make);
            Assert.NotEqual(carBeforeUpdate.Model, carAfterUpdate.Model);
            Assert.NotEqual(carBeforeUpdate.Price, carAfterUpdate.Price);
            Assert.NotEqual(carBeforeUpdate.Year, carAfterUpdate.Year);
        }
    }
}
