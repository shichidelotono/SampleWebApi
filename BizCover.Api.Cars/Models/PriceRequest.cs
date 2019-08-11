using System.Collections.Generic;

namespace BizCover.Api.Cars.Models
{
    public class PriceRequest
    {
        public IEnumerable<int> Ids { get; set; }
    }
}
