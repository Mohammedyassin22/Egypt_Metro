using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IFaultService
    {
        Task<FaultDto> AddFaultAsync(FaultDto dto);
        Task<IEnumerable<FaultDto>> GetAllFaultsAsync();
        Task<FaultDto> DeleteAsync(int id);
    }

}
