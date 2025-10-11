using Domain.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Presistense.Rebository
{
    public class CacheRebository(IConnectionMultiplexer connection) : ICacheRebository
    {
        private readonly IDatabase database=connection.GetDatabase();
        public async Task<string> GetAsync(string key)
        {
            var Value=await database.StringGetAsync(key);
            return !Value.IsNullOrEmpty ? Value : default;
        }

        public async Task SetAsync(string key, string value, TimeSpan Duration)
        {
            var rediesvalue=JsonSerializer.Serialize(value);
            await database.StringSetAsync(key, rediesvalue,Duration);
        }
    }
}
