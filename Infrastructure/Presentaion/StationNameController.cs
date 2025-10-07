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
    [Route("api/[controller]")]
    public class StationNameController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllStationsName")]
        public async Task<IActionResult> GetAllStationsAsync()
        {
            var stations= await serivcesManager.StationNameServices.GetAllStationsAsync();
            return Ok(stations);
        }

        [HttpGet("GetStationByName/{stationName}")]
        public async Task<IActionResult> GetStationNameAsync (string stationName)
        {
            var result= await serivcesManager.StationNameServices.GetStationNameAsync(stationName);
            if (result is null)
            {
                return NotFound();   
            }
            return Ok(result);
        }

        [HttpPost("AddStationName")]
        public async Task<IActionResult> AddStationNameAsync([FromBody] string stationName)
        {
            try
            {
                var station = await serivcesManager.StationNameServices.AddStationWithCoordinatesAsync(stationName);
                return Ok(station);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteStationName/{id}")]
        public async Task<IActionResult> DeleteStationNameAsync(int id)
        {
            try
            {
                var result = await serivcesManager.StationNameServices.DeleteAsync(id);

                if (result is null)
                    return NotFound($"Station name with ID {id} not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
