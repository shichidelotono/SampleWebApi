using BizCover.Api.Cars.Domains;
using BizCover.Api.Cars.Models;
using BizCover.Api.Cars.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _carsService.GetAll();

            var carResponses = new List<CarsResponse>();

            var sync = new object();

            Parallel.ForEach(result, c => {
                lock (sync) {
                    carResponses.Add(c.ToCarsResponse());
                };
            });

            return Ok(new GetCarsResponse { Cars = carResponses });
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddCarRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var carDomain = new CarDomain(null, request.Colour, request.CountryManufactured, request.Make, request.Model, request.Price, request.Year);

            var result = await _carsService.Add(carDomain);

            return Ok(new AddCarResponse
            {
                IsSuccess = result.IsSuccess,
                ErrorMessage = result.IsSuccess ? string.Empty : result.Message
            });
        }

        // PUT api/<controller>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateCarRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var carDomain = new CarDomain(id, request.Colour, request.CountryManufactured, request.Make, request.Model, request.Price, request.Year);

            var result = await _carsService.Update(carDomain);

            return Ok(new AddCarResponse
            {
                IsSuccess = result.IsSuccess,
                ErrorMessage = result.IsSuccess ? string.Empty : result.Message
            });
        }
    }
}
