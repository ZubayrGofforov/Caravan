using Caravan.Service.Dtos.Accounts;
using Caravan.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caravan.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IAccountService accountService;
        public UserController(IUserService service, IAccountService accountservice)
        {
            this.service = service;
            this.accountService = accountservice;
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetAsync(long id)
            => Ok(await service.GetAsync(id));
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await service.DeleteAsync(id));
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] AccountRegisterDto dto)
            => Ok(await service.UpdateAsync(id, dto));
    }
}
