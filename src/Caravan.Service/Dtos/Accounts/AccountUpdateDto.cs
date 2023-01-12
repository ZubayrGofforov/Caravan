using Caravan.Domain.Entities;
using Caravan.Service.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Dtos.Accounts
{
    public class AccountUpdateDto
    {
        [MaxLength(25), MinLength(3)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(25), MinLength(3)]
        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [PhoneNumberAttribute]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAttribute]
        public string Email { get; set; } = string.Empty;

        [MinLength(8), StrongPassword]
        public string Password { get; set; } = string.Empty;

        public static implicit operator User(AccountUpdateDto accountUpdateDto)
        {
            return new User()
            {
                FirstName = accountUpdateDto.FirstName,
                LastName = accountUpdateDto.LastName,
                Address = accountUpdateDto.Address,
                PhoneNumber = accountUpdateDto.PhoneNumber,
                Email = accountUpdateDto.Email
            };
        }
    }
}
