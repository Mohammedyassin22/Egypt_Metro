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
    public class FaultService(IMapper mapper, IUnitOfWork unitOfWork) : IFaultService
    {
        public async Task<FaultDto> AddFaultAsync(FaultDto dto)
        {
            var lineRepo = unitOfWork.GetRepository<Line_Name, int>();
            var line = (await lineRepo.GetAllAsync())
                       .FirstOrDefault(l => l.LineName == dto.LineName);

            if (line == null)
                throw new InvalidOperationException($"Line '{dto.LineName}' not found.");

            var faultEntity = mapper.Map<Faults>(dto);
            faultEntity.LineId = line.Id;

            var faultRepo = unitOfWork.GetRepository<Faults, int>();
            await faultRepo.AddAsync(faultEntity);
            await unitOfWork.SaveChangeAsync();

            var resultDto = mapper.Map<FaultDto>(faultEntity);
            resultDto.LineName = dto.LineName;

            return resultDto;
        }

        public async Task<IEnumerable<FaultDto>> GetAllFaultsAsync()
        {
            var faults = await unitOfWork.GetRepository<Faults, int>().GetAllAsync();

            if (faults == null || !faults.Any())
                return Enumerable.Empty<FaultDto>();

            var lines = await unitOfWork.GetRepository<Line_Name, int>().GetAllAsync();

            var result = faults.Select(f => new FaultDto
            {
                LineName = lines.FirstOrDefault(l => l.Id == f.LineId)?.LineName,
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
