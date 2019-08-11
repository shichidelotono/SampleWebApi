namespace BizCover.Api.Cars.Domains
{
    public class CarDomain
    {
        public int? Id { get; }
        public string Colour { get; }
        public string CountryManufactured { get; }
        public string Make { get; }
        public string Model { get; }
        public decimal Price { get; }
        public int Year { get; }

        public CarDomain(int? id, string colour, string countryManufactured, string make, string model, decimal price, int year)
        {
            Id = id;
            Colour = colour;
            CountryManufactured = countryManufactured;
            Make = make;
            Model = model;
            Price = price;
            Year = year;
        }
    }
}
