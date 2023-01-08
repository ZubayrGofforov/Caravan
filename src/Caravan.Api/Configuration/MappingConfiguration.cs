using AutoMapper;
using Caravan.Domain.Common;
using Caravan.Domain.Entities;
using Caravan.Service.Dtos;
using Caravan.Service.Dtos.Accounts;
using Caravan.Service.ViewModels;

namespace Caravan.Api.Configuration
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<TruckCreateDto, Truck>().ReverseMap();
            CreateMap<TruckViewModel, Truck>().ReverseMap();
            CreateMap<OrderCreateDto, Order>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<OrderViewModel, Order>().ReverseMap();
            CreateMap<LocationCreateDto, Location>().ReverseMap();
            CreateMap<LocationViewModel, Location>().ReverseMap();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
