using Domain.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.ErrorModels;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
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
