using Domain.Contracts;
using Domain.Modules;
using Presistense;
using ServicesAbstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StationNotificationService(IUnitOfWork _unitOfWork) : IStationNotificationService
    {
        public async Task<StationNotificationDto> CheckProximityAsync(UserTripDto userTrip)
        {
            var stationRepo = _unitOfWork.GetRepository<Station_Name, int>();
            var targetStation = await stationRepo.GetByConditionAsync(s => s.StationName == userTrip.TargetStationName);

            if (targetStation == null)
            {
                return new StationNotificationDto
                {
                    Message = $"❌No station found with name {userTrip.TargetStationName}.",
                    StationName = userTrip.TargetStationName
                };
            }

            double distance = CalculateDistance(
                userTrip.CurrentLocation.Latitude,
                userTrip.CurrentLocation.Longitude,
                targetStation.Coordinates.Latitude,
                targetStation.Coordinates.Longitude
            );

            if (distance <= 0.3)
            {
                return new StationNotificationDto
                {
                    Message = $"🚉 اقتربت من محطة {targetStation.StationName}, استعد للنزول.",
                    StationName = targetStation.StationName
                };
            }
        

        return new StationNotificationDto
        {
            Message = $"المسافة المتبقية إلى {targetStation.StationName}: {Math.Round(distance, 2)} كم.",
            StationName = targetStation.StationName
    };
        }
        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; 
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);
        
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
        
        private static double ToRadians(double angle) => angle * Math.PI / 180.0;
}

        }
    

