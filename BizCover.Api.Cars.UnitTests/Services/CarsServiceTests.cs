using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Repository;
using BizCover.Api.Cars.Repository.Dtos;
using BizCover.Api.Cars.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BizCover.Api.Cars.UnitTests.Services
{
    public class CarsServiceTests
    {
        private Mock<ICarsRepository> _mockCarsRepository;
        private CarsService _carsService;

        public CarsServiceTests()
        {
            _mockCarsRepository = new Mock<ICarsRepository>();
            _carsService = new CarsService(_mockCarsRepository.Object);
        }

        [Fact]
        public async Task get_all_api_should_retrieve_all_cars_information_from_repository()
        {
            // setup
            _mockCarsRepository.Setup(c => c.GetAll()).Verifiable();

            // act
            await _carsService.GetAll();

            // assert
            _mockCarsRepository.Verify(c => c.GetAll(), Times.Once);
        }

        [Fact]
        public async Task given_id_get_api_should_retrieve_a_car_information_from_repository()
        {
            // setup
            _mockCarsRepository.Setup(c => c.Get(It.IsAny<int>())).Verifiable();

            // act
            await _carsService.Get(1);

            // assert
            _mockCarsRepository.Verify(c => c.Get(1), Times.Once);
        }

        [Fact]
        public async Task given_car_domain_obj_update_api_should_update_repository()
        {
            // setup
            var givenCarDomain = new CarDomain(1, "blue", "japan", "subaru", "outback", 10000m, 2009);
            var mockGetResult = new CarDomain(1, "silver", "japan", "subaru", "outback", 10000m, 2009);
            var mockRepositoryUpdateResult = new CarsRepositoryResultDto { IsSuccess = true };
            _mockCarsRepository.Setup(c => c.Get(It.IsAny<int>())).ReturnsAsync(mockGetResult);
            _mockCarsRepository.Setup(c => c.Update(It.IsAny<CarDomain>())).ReturnsAsync(mockRepositoryUpdateResult);

            // act
            var result = await _carsService.Update(givenCarDomain);

            // assert
            _mockCarsRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Once);
            _mockCarsRepository.Verify(c => c.Update(It.IsAny<CarDomain>()), Times.Once);
            Assert.Equal(result.IsSuccess, mockRepositoryUpdateResult.IsSuccess);
            Assert.Equal(result.Message, mockRepositoryUpdateResult.Message);
        }

        [Fact]
        public async Task given_car_domain_obj_update_api_throws_ex_should_return_correct_response()
        {
            // setup
            var givenCarDomain = new CarDomain(1, "blue", "japan", "subaru", "outback", 10000m, 2009);
            var mockGetResult = new CarDomain(1, "silver", "japan", "subaru", "outback", 10000m, 2009);
            _mockCarsRepository.Setup(c => c.Get(It.IsAny<int>())).ReturnsAsync(mockGetResult);
            _mockCarsRepository.Setup(c => c.Update(It.IsAny<CarDomain>())).ThrowsAsync(new System.Exception());

            // act
            var result = await _carsService.Update(givenCarDomain);

            // assert
            _mockCarsRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Once);
            _mockCarsRepository.Verify(c => c.Update(It.IsAny<CarDomain>()), Times.Once);
            Assert.False(result.IsSuccess);
            Assert.Equal("Update operation failed", result.Message);
        }

        [Fact]
        public async Task given_car_domain_obj_add_api_should_add_it_to_repository()
        {
            // setup
            var givenCarDomain = new CarDomain(1, "blue", "japan", "subaru", "outback", 10000m, 2009);
            _mockCarsRepository.Setup(c => c.Add(givenCarDomain)).ReturnsAsync(1);
            _mockCarsRepository.Setup(c => c.GetAll()).ReturnsAsync(new List<CarDomain>());

            // act
            var result = await _carsService.Add(givenCarDomain);

            // assert
            _mockCarsRepository.Verify(c => c.Add(givenCarDomain), Times.Once);
            _mockCarsRepository.Verify(c => c.GetAll(), Times.Once);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Message);
        }
    }
}
