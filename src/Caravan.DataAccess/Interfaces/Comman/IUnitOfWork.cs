using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.DataAccess.Interfaces.Comman
{
    public interface IUnitOfWork
    {
        public IAdministratorRepository Administrators { get; }

        public IOrderRepository Orders { get; }

        public ITruckRepository Trucks { get; }

        public IUserRepository Users { get; }

        public Task<int> SaveChangesAsync();
    }
}
