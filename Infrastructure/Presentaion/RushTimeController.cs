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
    public class RushTimeController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllRush")]
        public async Task<IActionResult> GetAllRushTimesAsync()
        {
            var rushTimes= await serivcesManager.RushTimeServices.GetRushAllAsync();
            return Ok(rushTimes);
        }

        [HttpPost]
        public async Task<IActionResult> AddRushTimeAsync(Rush_TimeDto dto)
        {
            try
            {
                var result = await serivcesManager.RushTimeServices.AddRushTimeAsync(dto);
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

        [HttpDelete("DeleteRushTime/{id}")]
        public async Task<IActionResult> DeleteRushTimeAsync(int id)
        {
            try
            {
                var result = await serivcesManager.RushTimeServices.DeleteAsync(id);

                if (result is null)
                    return NotFound($"Rush time with ID {id} not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
