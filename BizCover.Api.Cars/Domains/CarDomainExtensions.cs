using BizCover.Api.Cars.Models;
namespace BizCover.Api.Cars.Domains
{
    public static class CarDomainExtensions
    {
        public static CarsResponse ToCarsResponse(this CarDomain carDomain)
        {
            return new CarsResponse
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
