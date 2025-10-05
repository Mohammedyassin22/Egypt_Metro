using Domain.Modules;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IRushTimeServices
    {
        Task<IEnumerable<Rush_TimeDto>> GetRushAllAsync();
        Task<Rush_TimeDto> GetRushByNameAsync(string namestation);
        Task<Rush_TimeDto> AddRushTimeAsync(string stationName,string congestionLevel,string? notes);
    }
}
