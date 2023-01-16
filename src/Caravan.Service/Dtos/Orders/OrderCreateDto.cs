using Caravan.Domain.Entities;
using Caravan.Service.Common.Attributes;
using Caravan.Service.Dtos.Locations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Dtos.Orders
{
    public class OrderCreateDto
    {
        [Required(ErrorMessage = "Please enter valid name")]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        [Required]
        [CheckNumber]
        public double Weight { get; set; }

        public double? Size { get; set; }

        [Required]
        public bool IsTaken { get; set; } = false;

        public string? LocationName { get; set; }

        [Required]
        public LocationCreateDto CurrentlyLocation { get; set; } = default!;

        [Required]
        public LocationCreateDto TransferLocation { get; set; } = default!;
    }
}
