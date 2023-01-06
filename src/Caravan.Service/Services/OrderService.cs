using AutoMapper;
using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces.Common;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Attributes;
using Caravan.Service.Common.Exceptions;
using Caravan.Service.Common.Helpers;
using Caravan.Service.Dtos;
using Caravan.Service.Interfaces;
using Caravan.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public OrderService(IUnitOfWork dbContext, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = dbContext;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<bool> CreateAsync(OrderCreateDto createDto)
        {
            var user = await _unitOfWork.Users.FindByIdAsync(IdentitySingelton.currentId().userId);
            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var order = _mapper.Map<Order>(createDto);
            order.CreatedAt = TimeHelper.GetCurrentServerTime();

            _unitOfWork.Orders.Add(order);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var order = await _unitOfWork.Orders.FindByIdAsync(id);
            if (order is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");

            if(!string.IsNullOrEmpty(order.ImagePath))
                await _imageService.DeleteImageAsync(order.ImagePath);

            var res = await _unitOfWork.SaveChangesAsync();
            return res > 0;

        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        //public async Task<Order> GetAsync(long id)
        //{
        //    var order = await _unitOfWork.Orders.FindByIdAsync(id);
        //    if (order is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");


        //}

        public Task<bool> UpdateAsync(OrderCreateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
