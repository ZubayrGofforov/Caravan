﻿using AutoMapper;
using Caravan.Domain.Entities;
using Caravan.Service.Dtos;

namespace Caravan.Api.Configuration
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<TruckCreateDto, Truck>().ReverseMap();
            CreateMap<OrderCreateDto, Order>().ReverseMap();
        }
    }
}