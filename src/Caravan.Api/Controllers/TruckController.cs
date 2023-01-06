using Caravan.Service.Dtos;
using Caravan.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caravan.Api.Controllers
{
    [Route("api/trucks")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _service;
        public TruckController(ITruckService truckService)
        {
            this._service = truckService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] TruckCreateDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetAsync(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _service.DeleteAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] TruckCreateDto truckCreateDto)
            => Ok(await _service.UpdateAsync(id, truckCreateDto));
    }
}
