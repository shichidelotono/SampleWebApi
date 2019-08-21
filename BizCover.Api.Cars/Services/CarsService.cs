using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Repository;
using BizCover.Api.Cars.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _carsRepository;

        public CarsService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public async Task<List<CarDomain>> GetAll()
        {
            return await _carsRepository.GetAll();
        }

        public async Task<CarsServiceResultDto> Update(CarDomain carDomain)
        {
            try
            {
                var existingCar = await Get(carDomain.Id.Value);

                if (existingCar == null)
                {
                    return new CarsServiceResultDto
                    {
                        IsSuccess = false,
                        Message = "Car does not exist in database"
                    };
                }

                var result = await _carsRepository.Update(carDomain);

                return new CarsServiceResultDto
                {
                    IsSuccess = result.IsSuccess,
                    Message = result.Message
                };
            }
            catch (Exception ex)
            {
                return new CarsServiceResultDto
                {
                    IsSuccess = false,
                    Message = "Update operation failed"
                };
            }
        }

        public async Task<CarsServiceResultDto> Add(CarDomain carDomain)
        {
            var cars = await _carsRepository.GetAll();
            var countBeforeAdding = cars.Count;

            if (IsExisted(carDomain, cars))
            {
                return new CarsServiceResultDto
                {
                    IsSuccess = false,
                    Message = "Car is already in database"
                };
            }

            try
            {
                var countAfterAdding = await _carsRepository.Add(carDomain);
                var isSuccess = countAfterAdding > countBeforeAdding;

                return new CarsServiceResultDto
                {
                    IsSuccess = isSuccess,
                    Message = isSuccess ? string.Empty : $@"Inserting car with id {carDomain.Id} failed"
                };
            }
            catch (Exception ex)
            {
                return new CarsServiceResultDto
                {
                    IsSuccess = false,
                    Message = "Add operation failed"
                };
            }           
        }

        public async Task<CarDomain> Get(int id)
        {
            return await _carsRepository.Get(id);
        }

        private bool IsExisted(CarDomain carDomain, List<CarDomain> existingCars)
        {
            var isExisting = false;

            var sync = new object();

            Parallel.ForEach(existingCars, (c, state) => {
                if (c.Year == carDomain.Year &&
                    c.Make == carDomain.Make &&
                    c.Model == carDomain.Model &&
                    c.Price == carDomain.Price &&
                    c.Colour == carDomain.Colour &&
                    c.CountryManufactured == carDomain.CountryManufactured)
                {
                    lock (sync)
                    {
                        isExisting = true;
                    }
                    state.Break();
                }
            });

            return isExisting;
        }
    }
}
