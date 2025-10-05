using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ICongestionScheduleService
    {
        Task<CongestionScheduleDto> AddCongestionAsync(CongestionScheduleDto dto);
        Task<IEnumerable<CongestionScheduleDto>> GetAllCongestionAsync();
    }

}
