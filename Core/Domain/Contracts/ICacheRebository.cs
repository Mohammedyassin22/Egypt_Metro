using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRebository
    {
        Task SetAsync(string key, string value,TimeSpan Duration);
        Task <string> GetAsync(string key);
    }
}
