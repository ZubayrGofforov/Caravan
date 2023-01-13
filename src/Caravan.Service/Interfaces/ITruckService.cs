using Caravan.Domain.Entities;
using Caravan.Service.Common.Utils;
using Caravan.Service.Dtos.Trucks;
using Caravan.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Interfaces
{
    public interface ITruckService 
    {
        public Task<IEnumerable<Truck>> GetAllAsync(PaginationParams @paginationParams);

        public Task<TruckViewModel> GetAsync(long id);

        public Task<bool> DeleteAsync(long id);

        public Task<bool> CreateAsync(TruckCreateDto dto);

        public Task<bool> UpdateAsync(long id, TruckCreateDto updateDto);
    }
}
