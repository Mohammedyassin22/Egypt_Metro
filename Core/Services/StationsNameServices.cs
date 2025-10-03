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
    public class StationsNameServices(IUnitOfWork unitOfWork ,IMapper mapper) : IStationsNameServices
    {
        public async Task<IEnumerable<Station_NameDto>> GetAllStationsAsync()
        {
            var stations = await unitOfWork .GetRepository<Station_Name,int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<Station_Name>, IEnumerable<Station_NameDto>>(stations);
            return result;
        }

        public async Task<string> GetStationNameAsync(string name)
        {
            var station = await unitOfWork.GetRepository<Station_Name, int>().GetByConditionAsync(x=>x.StationName==name);
            if (station is null)
            {
                return null;
            }
            var result = mapper.Map<Station_NameDto>(station);
            return result.StationName;

        }
    }
}
