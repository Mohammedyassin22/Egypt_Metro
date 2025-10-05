using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ITicketPricesServices
    {
        Task<int?> GetPriceAsync(int numstations);
        Task<Ticket_PricesDto> AddTicketPriceAsync(Ticket_PricesDto newPriceDto);
    }
}
