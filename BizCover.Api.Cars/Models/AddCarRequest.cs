using System.ComponentModel.DataAnnotations;

namespace BizCover.Api.Cars.Models
{
    public class AddCarRequest
    {
        [Required]
        public string Colour { get; set; }
        [Required]
        public string CountryManufactured { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required] 
        public int Year { get; set; }
    }
}
