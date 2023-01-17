using AutoMapper;
using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces.Common;
using Caravan.Domain.Common;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Attributes;
using Caravan.Service.Common.Exceptions;
using Caravan.Service.Common.Helpers;
using Caravan.Service.Common.Utils;
using Caravan.Service.Dtos.Orders;
using Caravan.Service.Interfaces;
using Caravan.Service.Interfaces.Common;
using Caravan.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILocationService _locationService;
        public OrderService(IUnitOfWork dbContext, IPaginatorService paginatorService, IMapper mapper, IImageService imageService, ILocationService locationService)
        {
            this._unitOfWork = dbContext;
            this._paginator = paginatorService;
            this._mapper = mapper;
            this._imageService = imageService;
            _locationService = locationService;
        }

        public async Task<bool> CreateAsync(OrderCreateDto createDto)
        {
            var user = await _unitOfWork.Users.FindByIdAsync(HttpContextHelper.UserId);
            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var order = _mapper.Map<Order>(createDto);
            order.UserId = HttpContextHelper.UserId;
            order.CreatedAt = TimeHelper.GetCurrentServerTime();
            order.UpdatedAt = TimeHelper.GetCurrentServerTime();
            order.ImagePath = await _imageService.SaveImageAsync(createDto.Image!);

            var resultTaken = await _locationService.CreateAsync(createDto.CurrentlyLocation);
            if (resultTaken.IsSuccessful) order.TakenLocationId = resultTaken.Id;
            else throw new StatusCodeException(HttpStatusCode.BadRequest, "Location is invalid");

            var resultDelivery = await _locationService.CreateAsync(createDto.TransferLocation);
            if(resultDelivery.IsSuccessful) order.DeliveryLocationId = resultDelivery.Id;
            else throw new StatusCodeException(HttpStatusCode.BadRequest, "Location is invalid");

            var r = await Task.Run(() => _unitOfWork.Orders.Add(order));
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

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(PaginationParams @paginationParams)
        {
            var query = _unitOfWork.Orders.GetAll().OrderBy(x => x.CreatedAt)
                .AsNoTracking().ToList().ConvertAll(x => _mapper.Map<OrderViewModel>(x));
            var data = await _paginator.ToPagedAsync(query, @paginationParams.PageNumber, @paginationParams.PageSize);
            return data;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllByIdAsync(long id, PaginationParams paginationParams)
        {
            var query = _unitOfWork.Orders.Where(x => x.UserId == id).ToList();
            var res = query.Where(x => x.UserId == HttpContextHelper.UserId).ToList().ConvertAll(x => _mapper.Map<OrderViewModel>(x));
            var data = await _paginator.ToPagedAsync(res, paginationParams.PageNumber, paginationParams.PageSize);
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

        public async Task<bool> UpdateAsync(long id, OrderUpdateDto updateDto)
        {
            var order = await _unitOfWork.Orders.FindByIdAsync(id);
            if (order is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");

            if(HttpContextHelper.UserId == order.UserId || HttpContextHelper.UserRole != "User")
            {
                _unitOfWork.Orders.TrackingDeteched(order);

                order.Name = updateDto.Name;
                order.Size = updateDto.Size;
                order.Weight = updateDto.Weight;
                order.Id = order.Id;
                order.TakenLocationId = order.DeliveryLocationId;
                order.TakenLocationId = order.TakenLocationId;
                order.UpdatedAt = TimeHelper.GetCurrentServerTime();

                if (updateDto.Image is not null)
                {
                    await _imageService.DeleteImageAsync(order.ImagePath!);
                    order.ImagePath = await _imageService.SaveImageAsync(updateDto.Image);
                }

                var resTaken = await _locationService.UpdateAsync(order.TakenLocationId, updateDto.CurrentlyLocation);
                if (resTaken == false)
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Currently location not updated");

                var resDelivery = await _locationService.UpdateAsync(order.DeliveryLocationId, updateDto.TransferLocation);
                if (resDelivery == false)
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Transfer location not updated");

                _unitOfWork.Orders.Update(id, order);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.BadRequest, "Not allowed");
        }

        public async Task<bool> UpdateStatusAsync(long id, OrderStatusDto dto)
        {
            var order = await _unitOfWork.Orders.FindByIdAsync(id);
            if (order is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");

            if (order.UserId == HttpContextHelper.UserId || HttpContextHelper.UserRole != "User")
            {
                _unitOfWork.Orders.TrackingDeteched(order);
                order.IsTaken = dto.IsTaken;
                _unitOfWork.Orders.Update(id, order);
                var res = await _unitOfWork.SaveChangesAsync();
                return res > 0;
            }

            throw new StatusCodeException(HttpStatusCode.BadRequest, "Not allowed");
        }
    }
}
