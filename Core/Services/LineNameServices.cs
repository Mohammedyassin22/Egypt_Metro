using AutoMapper;
using Domain.Contracts;
using Domain.Modules;
using Microsoft.IdentityModel.Abstractions;
using ServicesAbstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LineNameServices(IUnitOfWork unitOfWork,IMapper mapper) : ILineNameServices
    {
        public async Task<Line_NameDto> AddTicketPriceAsync(Line_NameDto newlineDto)
        {
            var priceEntity = mapper.Map<Line_Name>(newlineDto);

            var repository = unitOfWork.GetRepository<Line_Name, int>();

            await repository.AddAsync(priceEntity);

            await unitOfWork.SaveChangeAsync();

            var resultDto = mapper.Map<Line_NameDto>(priceEntity);

            return resultDto;
        }

        public async Task<IEnumerable<Line_NameDto>> GetAllStationsAsync()
        {
            var line = await unitOfWork.GetRepository<Line_Name,int>().GetAllAsync();
            var result=mapper.Map<IEnumerable<Line_NameDto>>(line);
            return result;
        }

        public async Task<string> GetLineNameAsync(string name)
        {
            var line = await unitOfWork.GetRepository<Line_Name, int>().GetByConditionAsync(x=>x.LineName==name);
            if(line is null)
            {
                return null;
            }
            var result = mapper.Map<Line_NameDto>(line);
            return result.LineName;
        }
    }
}
