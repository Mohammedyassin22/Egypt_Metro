using AutoMapper;
using Domain.Contracts;
using Domain.Exception;
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
    public class RushTimeServices(IMapper mapper, IUnitOfWork unitOfWork) : IRushTimeServices
    {
           public async Task<Rush_TimeDto> AddRushTimeAsync(string stationName, string congestionLevel, string notes)
        {

            var stationRepo = unitOfWork.GetRepository<Station_Name, int>();
            var stationEntity = await stationRepo.GetByConditionAsync(s => s.StationName == stationName);

            if (stationEntity == null)
                throw new NotFoundStationName(stationName);

            if (!Enum.TryParse<CongestionLevel>(congestionLevel, true, out var parsedLevel))
                throw new ArgumentException($"congestion Level '{congestionLevel}'  Low, Medium, High");

            var rushEntity = new Rush_Times
            {
                Station_NameId = stationEntity.Id,
                LineId = 0, 
                ObservationTime = DateTime.Now,
                congestionLevel = parsedLevel,
                Notes = notes
            };

            var rushRepo = unitOfWork.GetRepository<Rush_Times, int>();
            await rushRepo.AddAsync(rushEntity);
            await unitOfWork.SaveChangeAsync();


            var result = mapper.Map<Rush_TimeDto>(rushEntity);
            result.StationName = stationEntity.StationName;

            return result;
        }

           public async Task<IEnumerable<Rush_TimeDto>> GetRushAllAsync()
        {
            var rushTimes = await unitOfWork.GetRepository<Rush_Times,int>().GetAllAsync();
            var result= mapper.Map<IEnumerable<Rush_TimeDto>>(rushTimes);
            return result;
        }

            public async Task<Rush_TimeDto> GetRushByNameAsync(string stationName)
            {
                var rushRepo = unitOfWork.GetRepository<Rush_Times, int>();

                var rushEntity = await rushRepo.GetByConditionAsync(r =>
                    r.Station_Name != null && r.Station_Name.StationName == stationName);

                if (rushEntity is null)
                    return null;

                var result = mapper.Map<Rush_TimeDto>(rushEntity);
                return result;
            }

        
    }
}
