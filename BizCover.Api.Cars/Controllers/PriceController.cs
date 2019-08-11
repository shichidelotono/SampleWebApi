using BizCover.Api.Cars.Models;
using BizCover.Api.Cars.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Controllers
{
    [Route("api/[controller]")]
    public class PriceController : Controller
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PriceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var price = await _priceService.CalculateSalePrice(request.Ids);
                return Ok(new PriceResponse { IsSuccess = true, Price = price });
            }
            catch (Exception ex)
            {
                return Ok(new PriceResponse { IsSuccess = false, ErrorMessage = ex.Message });
            }
        }
    }
}
