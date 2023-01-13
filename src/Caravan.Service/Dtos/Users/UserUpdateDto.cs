using Caravan.Domain.Entities;
using Caravan.Service.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Dtos.Users
{
    public class UserUpdateDto
    {
        [Required, MaxLength(25), MinLength(3)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(25), MinLength(3)]
        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [Required, PhoneNumberAttribute]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, EmailAttribute]
        public string Email { get; set; } = string.Empty;
    }
}
