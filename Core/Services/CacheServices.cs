using Domain.Contracts;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CacheServices(ICacheRebository cacheRebository) : ICacheServices
    {
        public async Task<string> GetCacheValueAsync(string key)
        {
            var Value=await cacheRebository.GetAsync(key);
            return Value == null ? null : Value;
        }

        public async Task SetCacheValueAsync(string key, string value, TimeSpan Duration)
        {
            await cacheRebository.SetAsync(key, value, Duration);
        }
    }
}
