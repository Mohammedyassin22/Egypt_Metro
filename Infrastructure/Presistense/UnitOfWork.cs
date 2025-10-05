using Domain.Contracts;
using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Presistense.Data;
using Presistense.Rebository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Presistense
{
    public class UnitOfWork(MetroDbContex metroDbContex) : IUnitOfWork
    {
        public IGenericRebository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            return (IGenericRebository<TEntity, TKey>)valuePairs.GetOrAdd(
            typeof(TEntity).Name,
               new GenericRebository<TEntity, TKey>(metroDbContex));
        }

        public async Task<int> SaveChangeAsync()
        {
            return await metroDbContex.SaveChangesAsync();
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
           return await metroDbContex.SaveChangesAsync(cancellationToken);
        }

        private readonly ConcurrentDictionary<string, object> valuePairs = new(); 
    }
}
