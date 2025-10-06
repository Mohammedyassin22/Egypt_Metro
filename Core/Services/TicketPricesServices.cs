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

        public async Task<Ticket_PricesDto> AddTicketPriceAsync(int numStations, int price)
        {
            var priceEntity = new Ticket_Prices
            {
                StationsNumber = numStations,
                Price = price
            };

            var repository = unitOfWork.GetRepository<Ticket_Prices, int>();
            await repository.AddAsync(priceEntity);

            await unitOfWork.SaveChangeAsync();

            var resultDto = mapper.Map<Ticket_PricesDto>(priceEntity);
            return resultDto;
        }
        public async Task<IEnumerable<Ticket_PricesDto>> GetAllTicketPricesAsync()
        {
            var repository = unitOfWork.GetRepository<Ticket_Prices, int>();

            var prices = await repository.GetAllAsync();

            var result = mapper.Map<IEnumerable<Ticket_PricesDto>>(prices);
            return result;
        }

        }
    }
