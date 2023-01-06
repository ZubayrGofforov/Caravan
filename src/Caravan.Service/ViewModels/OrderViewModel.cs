﻿using Caravan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.ViewModels
{
    public class OrderViewModel
    {
        public string Name { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        public double Weight { get; set; }

        public double? Size { get; set; }

        public bool IsTaken { get; set; } = false;

        public virtual User User { get; set; } = default!;

        public long TakenLocationId { get; set; }
        public virtual Location TakenLocation { get; set; } = default!;

        public long DeliveryLocationId { get; set; }
        public virtual Location DeliveryLocation { get; } = default!;
    }
}