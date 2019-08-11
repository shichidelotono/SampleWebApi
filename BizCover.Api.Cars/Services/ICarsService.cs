using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Repository.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Services
{
    public interface ICarsService
    {
        Task<CarDomain> Get(int id);
        Task<List<CarDomain>> GetAll();
        Task<CarsServiceResultDto> Add(CarDomain carDomain);
        Task<CarsServiceResultDto> Update(CarDomain carDomain);
    }
}
