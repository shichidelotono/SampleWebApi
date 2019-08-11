using BizCover.Api.Cars.Domains;
using BizCover.Repository.Cars;

namespace BizCover.Api.Cars.Repository.Extensions
{
    public static class BizCoverRepositoryCarExtensions
    {
        public static CarDomain ToCarDomain(this Car car)
        {
            return new CarDomain(car.Id, car.Colour, car.CountryManufactured, car.Make, car.Model, car.Price, car.Year);
        }

        public static Car ToCar(this CarDomain carDomain)
        {
            return new Car
            {
                Id = carDomain.Id ?? 0,
                Colour = carDomain.Colour,
                CountryManufactured = carDomain.CountryManufactured,
                Make = carDomain.Make,
                Model = carDomain.Model,
                Price = carDomain.Price,
                Year = carDomain.Year
            }; 
        }
    }
}
