using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ICacheServices
    {
        Task SetCacheValueAsync (string key, string value,TimeSpan Duration);
        Task <string>GetCacheValueAsync (string key);
    }
}
