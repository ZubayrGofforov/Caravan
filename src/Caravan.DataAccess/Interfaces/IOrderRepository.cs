using Caravan.DataAccess.Interfaces.Common;
using Caravan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.DataAccess.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<Order?> GetByLocationNameAsync(string locationName);
    }
}
