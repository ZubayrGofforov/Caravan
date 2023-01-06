using Caravan.DataAccess.DbContexts;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Exceptions;
using Caravan.Service.Common.Security;
using Caravan.Service.Dtos.Accounts;
using Caravan.Service.Interfaces;
using Caravan.Service.Interfaces.Common;
using Caravan.Service.Interfaces.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _repository;
        private readonly IAuthManager _authManager;
        private readonly IImageService _image;

        public AccountService(AppDbContext repository, IAuthManager authManager, IImageService image)
        {
            _repository = repository;
            _authManager = authManager;
            _image = image;
        }

        public async Task<string> LoginAsync(AccountLoginDto loginDto)
        {
            var user = await _repository.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found, Email is incorrect!");

            else
            {
                var hasherResult = PasswordHasher.Verify(loginDto.Password, user.Salt, user.PasswordHash);
                if (hasherResult)
                {
                    return _authManager.GenerateToken(user);
                }
                else throw new StatusCodeException(HttpStatusCode.BadRequest, "Password is wrong!");
            }

        }

        public async Task<bool> RegisterAsync(AccountRegisterDto registerDto)
        {
            var emailcheck = await _repository.Users.FirstOrDefaultAsync(x => x.Email == registerDto.Email);
            if (emailcheck is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Email alredy exist");

            var hasherResult = PasswordHasher.Hash(registerDto.Password);
            var user = (User)registerDto;
            user.PasswordHash = hasherResult.passwordHash;
            user.Salt = hasherResult.salt;

            _repository.Add(user);
            var databaseResult = await _repository.SaveChangesAsync();
            return databaseResult > 0;
        }
    }
}
