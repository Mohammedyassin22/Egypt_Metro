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
    public class Station_CoordinateServices(IUnitOfWork _unitOfWork,IMapper _mapper): IStation_CoordinatesServices
    {
        public async Task<Station_CoordinatesDto?> GetCoordinatesByStationName(string stationName)
        {
            var stationRepo = _unitOfWork.GetRepository<Station_Name, int>();
            var stationEntity = await stationRepo.GetByConditionAsync(s => s.StationName == stationName);

            if (stationEntity is null)
                return null; 
            var coordinatesRepo = _unitOfWork.GetRepository<Station_Coordinates, int>();
            var coordinateEntity = await coordinatesRepo.GetByConditionAsync(c => c.StationId == stationEntity.Id);

            if (coordinateEntity is null)
                return null; 
            var result= _mapper.Map<Station_CoordinatesDto>(coordinateEntity);
            return result;
        }
    }
    }

