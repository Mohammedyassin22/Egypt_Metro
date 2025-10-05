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
            var line = await lineRepo.GetAsync(dto.LineId);

            if (line == null)
                throw new Exception($"Line with ID {dto.LineId} not found.");

            var faultEntity = mapper.Map<Faults>(dto);

            var faultRepo = unitOfWork.GetRepository<Faults, int>();
            await faultRepo.AddAsync(faultEntity);
            await unitOfWork.SaveChangeAsync();

            return mapper.Map<FaultDto>(faultEntity);
        }

        public async Task<IEnumerable<FaultDto>> GetAllFaultsAsync()
        {
            var fault =await unitOfWork.GetRepository<Faults, int>().GetAllAsync();
            if (fault is null)
            {
                return null;
            }
            return mapper.Map<IEnumerable<FaultDto>>(fault);
        }
    }
}
