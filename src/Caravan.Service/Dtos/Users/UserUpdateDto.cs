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
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [PhoneNumberAttribute]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
