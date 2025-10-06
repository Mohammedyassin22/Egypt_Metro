using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RushTimeController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllRush")]
        public async Task<IActionResult> GetAllRushTimesAsync()
        {
            var rushTimes= await serivcesManager.RushTimeServices.GetRushAllAsync();
            return Ok(rushTimes);
        }

        [HttpPost]
        public async Task<IActionResult> AddRushTimeAsync([FromBody] string startTime, string endTime,string? notes)
        {
            try
            {
                var result = await serivcesManager.RushTimeServices.AddRushTimeAsync(startTime, endTime,notes);
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

        [HttpGet("GetRushByName/{stationName}")]
        public async Task<IActionResult> GetRushByNameAsync (string stationName)
        {
            var result= await serivcesManager.RushTimeServices.GetRushByNameAsync(stationName);
            if (result is null)
            {
                return NotFound();   
            }
            return Ok(result);
        }
    }
}
