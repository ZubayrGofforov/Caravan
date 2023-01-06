using Caravan.Service.Dtos;
using Caravan.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caravan.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] OrderCreateDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetAsync(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok( await _service.DeleteAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] OrderCreateDto dto)
            => Ok(await _service.UpdateAsync(id,dto));
    }
}
