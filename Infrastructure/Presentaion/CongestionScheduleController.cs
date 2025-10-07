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

        [HttpPost("AddCongestionSchedule")]
        public async Task<IActionResult> AddCongestionScheduleAsync(string name, string level, string? notes)
        {
            try
            {
                var result = await serivcesManager.CongestionScheduleService.AddCongestionAsync(name, level, notes);

                if (result is null)
                    return BadRequest("Failed to add congestion schedule.");

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCongestionSchedule/{id}")]
        public async Task<IActionResult> DeleteCongestionScheduleAsync(int id)
        {
            try
            {
                var result = await serivcesManager.CongestionScheduleService.DeleteAsync(id);

                if (result is null)
                    return NotFound($"Congestion schedule with ID {id} not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
