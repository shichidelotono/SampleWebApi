using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Services
{
    public interface IPriceService
    {
        Task<decimal> CalculateSalePrice(IEnumerable<int> carIds);
    }
}