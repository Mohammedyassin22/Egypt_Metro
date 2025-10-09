using AutoMapper;
using Domain.Contracts;
using Domain.Modules;
using Microsoft.AspNetCore.SignalR;
using Services.Hubs;
using Services.Specifcation;
using ServicesAbstraction;
using ServicesAbstraction.Twilio;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Services
{
    public class CongestionScheduleService(IMapper mapper, IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext, ISmsService smsService /*IRepository<AppUser, int> _userRepository*/) : ICongestionScheduleService
    {
        public async Task<CongestionScheduleDto> AddCongestionAsync(string name, string level, string? notes)
        {
            var stationRepo = unitOfWork.GetRepository<Station_Name, int>();
            var spec = new StationNameSpecification(name);

            var stations = await stationRepo.GetAllAsync(spec);
            var station = stations.FirstOrDefault();

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

            //==================================================================
            await hubContext.Clients.All.SendAsync("ReceiveMessage", $"New congestion {name}: {notes}");

            //var users = await _userRepository.GetAllAsync();
            //foreach (var user in users)
            //{
            //    if (!string.IsNullOrEmpty(user.PhoneNumber))
            //    {
            //        await smsService.SendSmsAsync(
            //            user.PhoneNumber,
         //               $"New congestion {name}: {notes}")
            //        );
            //    }}
            return result;
        }

        public async Task<IEnumerable<CongestionScheduleDto>> GetAllCongestionAsync()
        {
            var spec=new CongestionScheduleSpecification();
            var congestionRepo = await unitOfWork.GetRepository<CongestionSchedule, int>().GetAllAsync(spec);
            var specstation = new StationNameSpecification();
            var stations = await unitOfWork.GetRepository<Station_Name, int>().GetAllAsync(specstation);

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
