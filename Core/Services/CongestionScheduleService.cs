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
using static System.Collections.Specialized.BitVector32;

namespace Services
{
    public class CongestionScheduleService(IMapper mapper, IUnitOfWork unitOfWork) : ICongestionScheduleService
    {
        public async Task<CongestionScheduleDto> AddCongestionAsync(string name, string level, string? notes)
        {
            var stationRepo = unitOfWork.GetRepository<Station_Name, int>();

            var stations = await stationRepo.GetAllAsync();
            var station = stations.FirstOrDefault(s => s.StationName == name);

            if (station == null)
                throw new Exception($"Station with name '{name}' not found.");

            // نحاول نحول level إلى enum
            if (!Enum.TryParse<CongestionLevels>(level, true, out var parsedLevel))
                throw new ArgumentException($"Invalid congestion level '{level}'. Use: Low, Medium, or High.");

            var congestionEntity = new CongestionSchedule
            {
                StationNameId = station.Id,       
                ObservationTime = DateTime.Now, 
                CongestionLevel = parsedLevel,  
                Notes = notes                   
            };

            var congestionRepo = unitOfWork.GetRepository<CongestionSchedule, int>();
            await congestionRepo.AddAsync(congestionEntity);
            await unitOfWork.SaveChangeAsync();

            var result = mapper.Map<CongestionScheduleDto>(congestionEntity);
            result.StationName = station.StationName; 

            return result;
        }

        public async Task<IEnumerable<CongestionScheduleDto>> GetAllCongestionAsync()
        {
            var congestionRepo = await unitOfWork.GetRepository<CongestionSchedule, int>().GetAllAsync();

            var stations = await unitOfWork.GetRepository<Station_Name, int>().GetAllAsync();

            var result = congestionRepo.Select(cs => new CongestionScheduleDto
            {
                StationName = stations.FirstOrDefault(s => s.Id == cs.StationNameId)?.StationName,
                congestionLevel = cs.CongestionLevel.ToString(), // هنا اتحولت string
                Notes = cs.Notes
            });
            return result;
        }

        public async Task<CongestionScheduleDto> DeleteAsync(int id)
        {
            var scheduleRepo = unitOfWork.GetRepository<CongestionSchedule, int>();

            var scheduleEntity = await scheduleRepo.GetAsync(id);

            if (scheduleEntity == null)
                throw new KeyNotFoundException($"No congestion schedule found with Id {id}.");

            scheduleRepo.Delete(scheduleEntity);

            await unitOfWork.SaveChangeAsync();

            var result = mapper.Map<CongestionScheduleDto>(scheduleEntity);
            return result;
        }

    }
}
