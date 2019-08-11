namespace BizCover.Api.Cars.Models
{
    public class PriceResponse : ResponseBase
    {
        public decimal Price { get; set; }
        public string Message { get; set; }
    }
}
