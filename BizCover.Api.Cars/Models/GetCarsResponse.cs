using System.Collections.Generic;
using System.Linq;

namespace BizCover.Api.Cars.Models
{
    public class GetCarsResponse
    {
        public int Total => Cars.Count(); 
        public IEnumerable<CarsResponse> Cars { get; set; }
    }
}
