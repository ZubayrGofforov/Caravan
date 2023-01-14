using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces.Common;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Attributes;
using Caravan.Service.Common.Exceptions;
using Caravan.Service.Common.Helpers;
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
        private readonly IUnitOfWork _repository;
        private readonly IAuthManager _authManager;
        private readonly IImageService _image;
        IdentitySingelton identity = new IdentitySingelton();
        

        public AccountService(IUnitOfWork repository, IAuthManager authManager, IImageService image)
        {
            _repository = repository;
            _authManager = authManager;
            _image = image;
        }

        public async Task<string> LoginAsync(AccountLoginDto loginDto)
        {
            var user = await _repository.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found, Email is incorrect!");

            var hasherResult = PasswordHasher.Verify(loginDto.Password, user.Salt, user.PasswordHash);
            if (hasherResult)
            {
                return _authManager.GenerateToken(user);
                
            }
            else throw new StatusCodeException(HttpStatusCode.BadRequest, "Password is wrong!");

        }

        public async Task<bool> PasswordUpdateAsync(PasswordUpdateDto passwordUpdateDto)
        {
            var user = await _repository.Users.FindByIdAsync(3);
            if (user is not null)
            {
                var result = PasswordHasher.Verify(passwordUpdateDto.OldPassword, user.Salt, user.PasswordHash);
                if (result)
                {
                    if (passwordUpdateDto.NewPassword == passwordUpdateDto.VerifyPassword)
                    {
                        var hash = PasswordHasher.Hash(passwordUpdateDto.VerifyPassword);
                        user.Salt = hash.salt;
                        user.PasswordHash = hash.passwordHash;
                        _repository.Users.Update(user.Id, user);
                        var res = await _repository.SaveChangesAsync();
                        return res > 0;
                    }
                    else throw new StatusCodeException(HttpStatusCode.BadRequest, "new password and verify password must be match");
                }
                else throw new StatusCodeException(HttpStatusCode.BadRequest, "Invalid Password");
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

        }

        public async Task<bool> RegisterAsync(AccountRegisterDto registerDto)
        {
            var emailcheck = await _repository.Users.FirstOrDefaultAsync(x => x.Email == registerDto.Email);
            if (emailcheck is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Email alredy exist");

            var hasherResult = PasswordHasher.Hash(registerDto.Password);
            var user = (User)registerDto;
            user.PasswordHash = hasherResult.passwordHash;
            user.Role = Domain.Enums.UserRole.User;
            user.Salt = hasherResult.salt;
            user.CreatedAt = TimeHelper.GetCurrentServerTime();
            user.UpdatedAt = TimeHelper.GetCurrentServerTime();
            _repository.Users.Add(user);
            var databaseResult = await _repository.SaveChangesAsync();
            return databaseResult > 0;
        }
    }
}
