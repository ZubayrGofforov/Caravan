using Caravan.DataAccess.DbContexts;
using Caravan.DataAccess.Interfaces.Common;
using Caravan.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.DataAccess.Repositories.Common
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected AppDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual void Add(T entity) => _dbSet.Add(entity) ;

        public virtual void Delete(long id)
        {
            var entity = _dbSet.Find(id);
            if(entity is not null)
                _dbSet.Remove(entity);
        }

        public virtual async Task<T?> FindByIdAsync(long id)
        {

            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public virtual void Update(long id, T entity)
        {
            entity.Id = id;
            _dbSet.Update(entity);
        }
    }
}
