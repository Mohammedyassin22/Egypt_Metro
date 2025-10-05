using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ILineNameServices
    {
        Task<string> GetLineNameAsync(string name);
        Task<IEnumerable<Line_NameDto>> GetAllStationsAsync();
        Task<Line_NameDto> AddTicketPriceAsync(Line_NameDto newlineDto);
    }
}
