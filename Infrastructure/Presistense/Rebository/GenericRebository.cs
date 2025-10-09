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

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification, bool trackchange = false)
        {
            IQueryable<TEntity> query;

            if (trackchange)
                query = metroDbContex.Set<TEntity>();
            else
                query = metroDbContex.Set<TEntity>().AsNoTracking();

            if (specification is not null)
                query = ApplicationsException(specification);

            return await query.ToListAsync();
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

        private IQueryable<TEntity> ApplicationsException(ISpecification<TEntity, TKey> spec)
        {
            return SpecificationEvalutor.GetQuery(metroDbContex.Set<TEntity>(), spec);
        }
    }
}
