using AutoMapper;
using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces.Common;
using Caravan.DataAccess.Migrations;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Attributes;
using Caravan.Service.Common.Exceptions;
using Caravan.Service.Common.Helpers;
using Caravan.Service.Common.Utils;
using Caravan.Service.Dtos;
using Caravan.Service.Interfaces;
using Caravan.Service.Interfaces.Common;
using Caravan.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Services
{
    public class TruckService : ITruckService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public TruckService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<bool> CreateAsync(TruckCreateDto dto)
        {
            var user = await _unitOfWork.Users.FindByIdAsync(IdentitySingelton.currentId().userId);
            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var truck = _mapper.Map<Truck>(dto);
            truck.CreatedAt = TimeHelper.GetCurrentServerTime();
            truck.ImagePath = await _imageService.SaveImageAsync(dto.Image!);

            _unitOfWork.Trucks.Add(truck);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var truck = await _unitOfWork.Trucks.FindByIdAsync(id);
            if (truck is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Truck not found");

            if (!string.IsNullOrEmpty(truck.ImagePath))
                await _imageService.DeleteImageAsync(truck.ImagePath);

            var res = await _unitOfWork.SaveChangesAsync();
            return res > 0;
        }

        public async Task<IEnumerable<Truck>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TruckViewModel> GetAsync(long id)
        {
            var truck = await _unitOfWork.Trucks.FindByIdAsync(id);
            if (truck is not null)
                return _mapper.Map<TruckViewModel>(truck);
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Track not found");
        }

        public async Task<bool> UpdateAsync(long id, TruckCreateDto updateDto)
        {
            var truck = await _unitOfWork.Trucks.FindByIdAsync(id);
            if (truck is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Truck not found");

            var updateTruck = _mapper.Map<Truck>(updateDto);

            if (updateDto.Image is not null)
            {
                await _imageService.DeleteImageAsync(truck.ImagePath!);
                updateTruck.ImagePath = await _imageService.SaveImageAsync(updateDto.Image);
            }
            updateTruck.Id = id;

            _unitOfWork.Trucks.Update(id, updateTruck);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}
