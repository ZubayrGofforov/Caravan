using AutoMapper;
using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces.Common;
using Caravan.Domain.Common;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Attributes;
using Caravan.Service.Common.Exceptions;
using Caravan.Service.Common.Helpers;
using Caravan.Service.Common.Utils;
using Caravan.Service.Dtos;
using Caravan.Service.Interfaces;
using Caravan.Service.Interfaces.Common;
using Caravan.Service.ViewModels;
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
        private readonly IPaginatorService _paginator;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public OrderService(IUnitOfWork dbContext, IPaginatorService paginatorService, IMapper mapper, IImageService imageService)
        {
            this._unitOfWork = dbContext;
            this._paginator = paginatorService;
            this._mapper = mapper;
            this._imageService = imageService;
        }

        public async Task<bool> CreateAsync(OrderCreateDto createDto)
        {
            var user = await _unitOfWork.Users.FindByIdAsync(createDto.UserId);
            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var order = _mapper.Map<Order>(createDto);
            order.CreatedAt = TimeHelper.GetCurrentServerTime();
            order.UpdatedAt = TimeHelper.GetCurrentServerTime();
            order.ImagePath = await _imageService.SaveImageAsync(createDto.Image!);

            //var res = await _unitOfWork.Locations.Add(_mapper.Map<Location>(createDto.CurrentlyLocation));

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

            _unitOfWork.Orders.Delete(id);

            var res = await _unitOfWork.SaveChangesAsync();
            return res > 0;

        }

        public async Task<IEnumerable<Order>> GetAllAsync(PaginationParams @paginationParams)
        {
            var query = _unitOfWork.Orders.GetAll().OrderBy(x => x.CreatedAt);
            var data = await _paginator.ToPagedAsync(query, @paginationParams.PageNumber, @paginationParams.PageSize);
            return data;
        }


        public async Task<OrderViewModel> GetAsync(long id)
        {
            var order = await _unitOfWork.Orders.FindByIdAsync(id);

            if (order is not null)
            {
                var res = _mapper.Map<OrderViewModel>(order);
                //res.TakenLocation.
                return res;

            }

            else throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");
        }

        public async Task<bool> UpdateAsync(long id, OrderCreateDto updateDto)
        {
            var order = await _unitOfWork.Orders.FindByIdAsync(id);
            if (order is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");

            var updateOrder = _mapper.Map<Order>(updateDto);

            if(updateDto.Image is not null)
            {
                await _imageService.DeleteImageAsync(order.ImagePath!);
                updateOrder.ImagePath = await _imageService.SaveImageAsync(updateDto.Image);
            }
            updateOrder.Id = id;

            _unitOfWork.Orders.Update(id, updateOrder);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}
