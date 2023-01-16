using Caravan.Service.Common.Utils;
using Caravan.Service.Dtos.Locations;
using Caravan.Service.Dtos.Trucks;
using Caravan.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Caravan.Api.Controllers
{
    [Route("api/trucks")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _service;
        private readonly int pageSize = 20;
        public TruckController(ITruckService truckService)
        {
            this._service = truckService;
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllAsync(int page)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, pageSize)));

        [HttpPost, Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAsync([FromForm] TruckCreateDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpGet("{id}"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetAsync(id));

        [HttpDelete("{id}"), Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _service.DeleteAsync(id));

        [HttpPatch("{id}"), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateStatusAsync(long id,TruckStatusDto status)
            => Ok(await _service.TruckStatusUpdateAsync(id, status));

        [HttpPut("{id}"), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] TruckUpdateDto truckUpdateDto)
            => Ok(await _service.UpdateAsync(id, truckUpdateDto));
        [HttpPatch, Authorize(Roles ="User")]
        public async Task<IActionResult> UpdateLocationAsync(long id, LocationCreateDto dto)
            => Ok(await _service.UpdateLocationAsync(id, dto));
        [HttpGet("{id},{page}")]
        public async Task<IActionResult> GetAllByIdAsync(long id, int page)
            => Ok(await _service.GetAllByIdAsync(id, new PaginationParams(page, pageSize)));
    }
}
