using AutoMapper;
using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces.Common;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Exceptions;
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
       
        public async Task<bool> DeleteAsync(int id)
        {
            var temp = await unitOfWork.Users.FindByIdAsync(id);
            if(temp is not null)
            {
                dbContext.Users.Remove(temp);
                var res = await dbContext.SaveChangesAsync();
                return res > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAysnc()
        {
            var users = await  dbContext.Users.AsNoTracking().ToListAsync();
            return (IEnumerable<UserViewModel>)mapper.Map<UserViewModel>(users);
        }

        public async Task<User> GetAsync(int id)
        {
            var temp = await unitOfWork.Users.FindByIdAsync(id);
            if (temp is not null)
                 return temp;
            else throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");

        }

        public async Task<bool> UpdateAsync(int id, UserViewModel entity)
        {
            var temp = await  unitOfWork.Users.FindByIdAsync(id);
            mapper.Map<User>(entity);
            if (temp is not null)
            {
                unitOfWork.Users.Update(mapper.Map<User>(entity));
                var res = await unitOfWork.SaveChangesAsync();
                return res > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
        }
    }
}
