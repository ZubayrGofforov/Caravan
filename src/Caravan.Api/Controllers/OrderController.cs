using Caravan.Service.Common.Utils;
using Caravan.Service.Dtos.Orders;
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
        private readonly int _pageSize = 20;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, _pageSize)));

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
