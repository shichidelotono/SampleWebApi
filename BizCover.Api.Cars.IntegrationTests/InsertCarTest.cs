using BizCover.Api.Cars.Controllers;
using BizCover.Api.Cars.Models;
using BizCover.Api.Cars.Repository;
using BizCover.Api.Cars.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BizCover.Api.Cars.IntegrationTests
{
    public class InsertCarTest
    {
        private BizCover.Repository.Cars.CarRepository _bizCoverCarRepository;
        private CarsRepository _carsRepository;
        private CarsService _carsService;
        private CarsController _carsController;

        public InsertCarTest()
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
            var givenRequest = new AddCarRequest
            {
                Colour = "silver",
                CountryManufactured = "japan",
                Make = "subaru",
                Model = "outback",
                Price = 8000m,
                Year = 2009
            };
            var carsBeforeAddingResult = await _carsController.Get();
            var carsBeforeAddingOkResult = carsBeforeAddingResult as OkObjectResult;
            var carsBeforeAdding = carsBeforeAddingOkResult.Value as GetCarsResponse;

            // act
            var result = await _carsController.Post(givenRequest);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as AddCarResponse;

            // verify 
            var carsAfterAddingResult = await _carsController.Get();
            var carsAfterAddingOkResult = carsAfterAddingResult as OkObjectResult;
            var carsAfterAdding = carsAfterAddingOkResult.Value as GetCarsResponse;
            Assert.Equal(200, okResult.StatusCode.Value);
            Assert.True(response.IsSuccess);
            Assert.Empty(response.ErrorMessage);
            Assert.Equal(carsBeforeAdding.Cars.Count() + 1, carsAfterAdding.Cars.Count());
        }
    }
}
