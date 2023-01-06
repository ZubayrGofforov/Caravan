using Caravan.Domain.Entities;
using Caravan.Service.Common.Utils;
using Caravan.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserViewModel>> GetAllAysnc(PaginationParams @params);
        public Task<User> GetAsync(long id);
        public Task<bool> UpdateAsync(long id, UserViewModel entity);
        public Task<bool> DeleteAsync(long id);
    }
}
