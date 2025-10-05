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
    public class CongestionScheduleService(IMapper mapper, IUnitOfWork unitOfWork) : ICongestionScheduleService
    {
        public async Task<CongestionScheduleDto> AddCongestionAsync(CongestionScheduleDto dto)
        {
            var stationRepo = unitOfWork.GetRepository<Station_Name, int>();
            var station = await stationRepo.GetAsync(dto.StationNameId);

            if (station == null)
                throw new Exception($"Station with ID {dto.StationNameId} not found.");

            if (!Enum.TryParse<CongestionLevels>(dto.congestionLevel, true, out var parsedLevel))
                throw new ArgumentException($"Invalid congestion level '{dto.congestionLevel}'. Use: Low, Medium, or High.");

            var congestionEntity = new CongestionSchedule
            {
                StationName = station.Id,
                ObservationTime = DateTime.Now,
                congestionLevel = parsedLevel,
                Notes = dto.Notes
            };

            var congestionRepo = unitOfWork.GetRepository<CongestionSchedule, int>();
            await congestionRepo.AddAsync(congestionEntity);
            await unitOfWork.SaveChangeAsync();

            var result = mapper.Map<CongestionScheduleDto>(congestionEntity);
            result.StationNameId = station.Id;

            return result;
        }

        public Task<IEnumerable<CongestionScheduleDto>> GetAllCongestionAsync()
        {
            var congestionRepo = unitOfWork.GetRepository<CongestionSchedule, int>().GetAllAsync();
            if (congestionRepo is null)
            {
                return null;
            }
            var result = mapper.Map<IEnumerable<CongestionScheduleDto>>(congestionRepo);
            return Task.FromResult(result);

        }
    }
}
