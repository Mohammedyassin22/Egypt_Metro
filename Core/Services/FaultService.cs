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

namespace Services
{
    public class FaultService(IMapper mapper, IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext,ISmsService smsService /*IRepository<AppUser, int> _userRepository*/) : IFaultService
    {
        public async Task<FaultDto> AddFaultAsync(FaultDto dto)
        {
            var spec = new LineNameSpecification(dto.LineName);

            var lineRepo = unitOfWork.GetRepository<Line_Name, int>();
            var line = (await lineRepo.GetAllAsync(spec)).FirstOrDefault();

            if (line == null)
                throw new InvalidOperationException($"Line '{dto.LineName}' not found.");

            var faultEntity = mapper.Map<Faults>(dto);
            faultEntity.LineId = line.Id;

            var faultRepo = unitOfWork.GetRepository<Faults, int>();
            await faultRepo.AddAsync(faultEntity);
            await unitOfWork.SaveChangeAsync();

            var resultDto = mapper.Map<FaultDto>(faultEntity);
            resultDto.LineName = dto.LineName;

            // =============================
            await hubContext.Clients.All.SendAsync("ReceiveMessage", $"🚨 عطل جديد على خط {dto.LineName}: {dto.Description}");

            //var users = await _userRepository.GetAllAsync();
            //foreach (var user in users)
            //{
            //    if (!string.IsNullOrEmpty(user.PhoneNumber))
            //    {
            //        await smsService.SendSmsAsync(
            //            user.PhoneNumber,
            //            $"🚨 تم تسجيل عطل جديد على خط {dto.LineName}: {dto.Description}"
            //        );
            //    }}
            return resultDto;
        }

        public async Task<IEnumerable<FaultDto>> GetAllFaultsAsync()
        {
            var spec = new FaultsSpecification();
            var faultRepo = unitOfWork.GetRepository<Faults, int>();
            var faults = await faultRepo.GetAllAsync(spec);

            if (faults == null || !faults.Any())
                return Enumerable.Empty<FaultDto>();

            var result = faults.Select(f => new FaultDto
            {
                LineName = f.Line?.LineName, // بفضل الـ Include بقت موجودة هنا
                Title = f.Title,
                StartTime = f.StartTime,
                EndTime = f.EndTime,
                Description = f.Description
            });

            return result;
        }


        public async Task<FaultDto> DeleteAsync(int id)
        {
            var faultrepo=unitOfWork.GetRepository<Faults, int>();
            var faultEntity = await faultrepo.GetAsync(id);
            if (faultEntity == null)
                throw new KeyNotFoundException($"No fault found with ID {id}.");
            faultrepo.Delete(faultEntity);
            await unitOfWork.SaveChangeAsync();
            var resultDto = mapper.Map<FaultDto>(faultEntity);
            return resultDto;
        }
    }
}
