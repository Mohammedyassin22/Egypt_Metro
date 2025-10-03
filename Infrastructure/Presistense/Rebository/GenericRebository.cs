using Domain.Contracts;
using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Presistense.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistense.Rebository
{
    public class GenericRebository<TEntity, TKey>(MetroDbContex metroDbContex) : IGenericRebository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        {
            await metroDbContex.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            metroDbContex.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackchange = false)
        {
            if(trackchange)
                return await metroDbContex.Set<TEntity>().ToListAsync();
            return await metroDbContex.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await metroDbContex.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await metroDbContex.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public void Update(TEntity entity)
        {
            metroDbContex.Update(entity);
        }
    }
}
