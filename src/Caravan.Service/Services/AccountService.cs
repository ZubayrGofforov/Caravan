using Caravan.DataAccess.DbContexts;
using Caravan.Service.Dtos.Accounts;
using Caravan.Service.Interfaces;
using Caravan.Service.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _repository;
        private readonly IAuthManager _authManager;
        public Task<bool> LoginAsync(AccountLoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(AccountRegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
