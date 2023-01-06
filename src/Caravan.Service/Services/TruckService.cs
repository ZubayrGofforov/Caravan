using Caravan.DataAccess.DbContexts;
using Caravan.Domain.Entities;
using Caravan.Service.Common.Exceptions;
using Caravan.Service.Common.Utils;
using Caravan.Service.Dtos;
using Caravan.Service.Interfaces;
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
        private readonly AppDbContext _appDbContext;
        public TruckService(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<bool> CreateAsync(TruckCreateDto dto)
        {
            var entity = (Truck)dto;
            _appDbContext.Trucks.Add(entity);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _appDbContext.Trucks.FindAsync(id);
            if(entity is not null)
            {
                _appDbContext.Trucks.Remove(entity);
                var result = await _appDbContext.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Truck not found");
        }

        public async Task<IEnumerable<Truck>> GetAllAsync()
        {
            var quary = _appDbContext.Trucks.OrderBy(x => x.Id);

            return await quary.ThenByDescending(x => x.IsEmpty)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Truck> GetAsync(long id)
        {
            var result = await _appDbContext.Trucks.FindAsync(id);
            if (result is null) throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Truck not found");
            return result;
        }

        public async Task<bool> UpdateAsync(long id, Truck obj)
        {
            var entity = await _appDbContext.Trucks.FindAsync(id);
            if (entity is not null)
            {
                _appDbContext.Entry<Truck>(entity!).State = EntityState.Detached;
                obj.Id = id;
                _appDbContext.Trucks.Update(obj);
                var result = await _appDbContext.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Car not found");
        }
    }
}
