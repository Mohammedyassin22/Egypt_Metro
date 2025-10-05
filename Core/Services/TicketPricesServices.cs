using AutoMapper;
using Domain.Contracts;
using Domain.Modules;
using ServicesAbstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TicketPricesServices(IUnitOfWork unitOfWork, IMapper mapper) : ITicketPricesServices
    {
        public async Task<Ticket_PricesDto> AddTicketPriceAsync(Ticket_PricesDto newPriceDto)
        {
            var priceEntity = mapper.Map<Ticket_Prices>(newPriceDto);

            var repository = unitOfWork.GetRepository<Ticket_Prices, int>();

            await repository.AddAsync(priceEntity);

            await unitOfWork.SaveChangeAsync();

            var resultDto = mapper.Map<Ticket_PricesDto>(priceEntity);

            return resultDto;
        }

        public async Task<int?> GetPriceAsync(int numstations)
        {
            var price = await unitOfWork.GetRepository<Ticket_Prices, int>().GetAsync(numstations);
            if(price is null)
            {
                return null;
            }
            var result = mapper.Map<Ticket_PricesDto>(price);
            return result.Price;
        }
    }
}
