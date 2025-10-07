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
          public async Task<Rush_TimeDto> AddRushTimeAsync(Rush_TimeDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // جلب المحطة
            var stationRepo = unitOfWork.GetRepository<Station_Name, int>();
            var stationEntity = await stationRepo.GetByConditionAsync(s => s.StationName == dto.StationName);
            if (stationEntity == null)
                throw new Exception($"Station '{dto.StationName}' not found.");

            // جلب الخط
            var lineRepo = unitOfWork.GetRepository<Line_Name, int>();
            var lineEntity = await lineRepo.GetByConditionAsync(l => l.LineName == dto.LineName);
            if (lineEntity == null)
                throw new Exception($"Line '{dto.LineName}' not found.");

            // التأكد من وجود علاقة المحطة والخط
            var stationLineRepo = unitOfWork.GetRepository<Stations_Lines, int>();
            var stationLine = await stationLineRepo.GetByConditionAsync(sl =>
    sl.StationNameId == stationEntity.Id && sl.LineId == lineEntity.Id);

            if (stationLine == null)
            {
                stationLine = new Stations_Lines
                {
                    StationNameId = stationEntity.Id,
                    LineId = lineEntity.Id
                };
                await stationLineRepo.AddAsync(stationLine);
                await unitOfWork.SaveChangeAsync();
            }

            // تحويل مستوى الازدحام
            if (!Enum.TryParse<CongestionLevel>(dto.congestionLevel, true, out var parsedLevel))
                throw new ArgumentException($"Congestion Level must be Low, Medium, or High. Value received: '{dto.congestionLevel}'");

            // إنشاء Rush Time
            var rushRepo = unitOfWork.GetRepository<Rush_Times, int>();
            var rushEntity = new Rush_Times
            {
                Station_NameId = stationEntity.Id,
                LineId = lineEntity.Id,
                ObservationTime = dto.ObservationTime,
                congestionLevel = parsedLevel,
                Notes = dto.Notes
            };

            await rushRepo.AddAsync(rushEntity);
            await unitOfWork.SaveChangeAsync();

            var result = new Rush_TimeDto
            {
                StationName = stationEntity.StationName,
                LineName = lineEntity.LineName,
                ObservationTime = rushEntity.ObservationTime,
                congestionLevel = rushEntity.congestionLevel.ToString(),
                Notes = rushEntity.Notes
            };

            return result;
        }

        public async Task<Rush_TimeDto> DeleteAsync(int id)
        {
            var faultrepo = unitOfWork.GetRepository<Rush_Times, int>();
            var faultEntity = await faultrepo.GetAsync(id);
            if (faultEntity == null)
                throw new KeyNotFoundException($"No fault found with ID {id}.");
            faultrepo.Delete(faultEntity);
            await unitOfWork.SaveChangeAsync();
            var resultDto = mapper.Map<Rush_TimeDto>(faultEntity);
            return resultDto;
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
