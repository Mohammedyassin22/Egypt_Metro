using Domain.Contracts;
using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistense
{
    public static class SpecificationEvalutor
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>
                   (IQueryable<TEntity> inpuyquery, ISpecification<TEntity, TKey> spec)
                   where TEntity : BaseEntity<TKey>
        {
            var query = inpuyquery;
            if (spec.criterial is not null)
            {
                query = query.Where(spec.criterial);
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
        }
}
