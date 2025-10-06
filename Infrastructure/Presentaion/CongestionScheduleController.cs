using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CongestionScheduleController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllCongestionSchedules")]
        public async Task<IActionResult> GetAllCongestionSchedulesAsync()
        {
            var schedules= await serivcesManager.CongestionScheduleService.GetAllCongestionAsync();
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<IActionResult> AddCongestionScheduleAsync(CongestionScheduleDto dto)
        {
            try
            {
                var result = await serivcesManager.CongestionScheduleService.AddCongestionAsync(dto);
                if(result is null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
