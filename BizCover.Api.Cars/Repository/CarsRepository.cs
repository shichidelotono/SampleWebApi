using BizCover.Api.Cars.Cache;
using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Repository.Dtos;
using BizCover.Api.Cars.Repository.Extensions;
using BizCover.Repository.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Repository
{
    public class CarsRepository : ICarsRepository
    {
        private readonly ICarRepository _bizCoverCarRepository;
        private CarsMemoryCache<List<Car>> _carsMemoryCache = new CarsMemoryCache<List<Car>>();
        private readonly string carsKey = Guid.NewGuid().ToString();

        public CarsRepository(ICarRepository bizCoverCarRepository)
        {
            _bizCoverCarRepository = bizCoverCarRepository;
        }

        public async Task<List<CarDomain>> GetAll()
        {
            var cars = await _carsMemoryCache.GetOrAdd(carsKey, () => _bizCoverCarRepository.GetAllCars());

            var result = new List<CarDomain>();

            var sync = new object();

            Parallel.ForEach(cars, c => {
                lock (sync)
                {
                    result.Add(c.ToCarDomain());
                }
            });

            return result;
        }

        public async Task<int> Add(CarDomain carDomain)
        {
            var totalNumberOfCarsInRepository = await _bizCoverCarRepository.Add(carDomain.ToCar());

            await _carsMemoryCache.Refresh(carsKey, () => _bizCoverCarRepository.GetAllCars());

            return totalNumberOfCarsInRepository;
        }

        public async Task<CarsRepositoryResultDto> Update(CarDomain carDomain)
        {
            try
            {
                await _bizCoverCarRepository.Update(carDomain.ToCar());

                await _carsMemoryCache.Refresh(carsKey, () => _bizCoverCarRepository.GetAllCars());
            }
            catch (Exception)
            {
                throw;
            }

            return new CarsRepositoryResultDto { IsSuccess = true };
        }

        public async Task<CarDomain> Get(int id)
        {
            var cars = await _carsMemoryCache.GetOrAdd(carsKey, () => _bizCoverCarRepository.GetAllCars());

            return cars?.FirstOrDefault(c => c.Id == id)?.ToCarDomain();
        }
    }
}
