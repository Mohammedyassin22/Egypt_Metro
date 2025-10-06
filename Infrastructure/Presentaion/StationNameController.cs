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
                var station = await serivcesManager.StationNameServices.GetStationNameAsync(stationName);
                if (station == null)
                    return NotFound();

                return Ok(station);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

   //     [HttpPost("AddTicketPrices")]
      /*  public async Task<IActionResult> AddTicketPriceAsync([FromBody] Station_NameDto newname)
        {
            var result = await serivcesManager.StationNameServices.AddTicketPriceAsync(newname);
            if(result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }*/

    }
}
