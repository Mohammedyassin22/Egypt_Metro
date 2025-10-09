using Domain.Modules;
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
    [Route("api/[controller]")]
    public class Station_CoordinateController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetCoordinatesByStationName")]
        public async Task<IActionResult> GetCoordinatesByStationName(string stationName)
        {
            var result = await serivcesManager.Station_CoordinatesServices.GetCoordinatesByStationName(stationName);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
