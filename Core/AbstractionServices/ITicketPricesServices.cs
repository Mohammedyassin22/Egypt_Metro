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
        Task<IEnumerable<Ticket_PricesDto>> GetAllTicketPricesAsync();
        Task<Ticket_PricesDto> AddTicketPriceAsync(int numStations, int price);
    }
}
