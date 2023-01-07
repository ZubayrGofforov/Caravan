using AutoMapper;
using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces.Common;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Exceptions;
using Caravan.Service.Common.Utils;
using Caravan.Service.Dtos.Accounts;
using Caravan.Service.Interfaces;
using Caravan.Service.Interfaces.Common;
using Caravan.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageService imageService;
        public UserService(IMapper imapper, IUnitOfWork unitOfWork, IImageService imageService)
        {
            this.mapper = imapper;
            this.unitOfWork = unitOfWork;
            this.imageService = imageService;
        }
       
        public async Task<bool> DeleteAsync(long id)
        {
            var temp = await unitOfWork.Users.FindByIdAsync(id);
            if(temp is not null)
            {
                unitOfWork.Users.Delete(id);
                var res = await unitOfWork.SaveChangesAsync();
                return res > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAysnc(PaginationParams @params)
        {
            var users = await unitOfWork.Users.GetAll().ToListAsync();
            return (IEnumerable<UserViewModel>)mapper.Map<UserViewModel>(users);
        }

        public async Task<UserViewModel> GetAsync(long id)
        {
            var temp = await unitOfWork.Users.FindByIdAsync(id);
            if (temp is not null)
                 return mapper.Map<UserViewModel>(temp);
            else throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");

        }

        public async Task<bool> UpdateAsync(long id, AccountRegisterDto entity)
        {
            var temp = await  unitOfWork.Users.FindByIdAsync(id);
            if (temp is not null)
            {
                unitOfWork.Users.Update(id, mapper.Map<User>(entity));
                var res = await unitOfWork.SaveChangesAsync();
                return res > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
        }
    }
}
