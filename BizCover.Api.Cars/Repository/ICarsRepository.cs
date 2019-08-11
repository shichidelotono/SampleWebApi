using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Repository.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Repository
{
    public interface ICarsRepository
    {
        Task<CarDomain> Get(int id);
        Task<List<CarDomain>> GetAll();
        Task<int> Add(CarDomain carDomain);
        Task<CarsRepositoryResultDto> Update(CarDomain carDomain);
    }
}
