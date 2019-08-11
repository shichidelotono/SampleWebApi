namespace BizCover.Api.Cars.Models
{
    public class CarsResponse
    {
        public int Id { get; set; }
        public string Colour { get; set; }
        public string CountryManufactured { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
    }
}
