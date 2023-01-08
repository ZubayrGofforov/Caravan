using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces;
using Caravan.DataAccess.Repositories.Common;
using Caravan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.DataAccess.Repositories
{
    public class TruckRepository : GenericRepository<Truck>, 
        ITruckRepository
    {
        public TruckRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public override async Task<Truck?> FindByIdAsync(long id)
        {
            var res = await _dbContext.Trucks.Include(x => x.User).FirstOrDefaultAsync(x => x.Id== id);
            if (res is null) return null; 
            return res;
        }

    }
}
