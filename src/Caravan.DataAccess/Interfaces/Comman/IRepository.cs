using Caravan.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.DataAccess.Interfaces.Comman
{
    public interface IRepository<T> where T : BaseEntity 
    {
        public void Add(T entity);

        public void Delete(long id);

        public void Update(long id, T entity);

        public Task<T?> FindByIdAsync(long id);

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    }
}
