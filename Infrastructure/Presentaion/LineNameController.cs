using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
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
    public class LineNameController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllLineNames")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetAllLineNamesAsync()
        {
            var lines= await serivcesManager.LineNameServices.GetAllStationsAsync();
            return Ok(lines);
        }

        [HttpGet("GetLineByName/{lineName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetLineNameAsync (string lineName)
        {
            var result= await serivcesManager.LineNameServices.GetLineNameAsync(lineName);
            if (result is null)
            {
                return NotFound();   
            }
            return Ok(result);
        }

        [HttpPost("AddLineName")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> AddLineNameAsync([FromBody] Line_NameDto newlineDto)
        {
            if (newlineDto == null)
                return BadRequest("Line data is required");

            try
            {
                var result = await serivcesManager.LineNameServices.AddTicketPriceAsync(newlineDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteLineName/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> DeleteLineNameAsync(int id)
        {
            try
            {
                var result = await serivcesManager.LineNameServices.DeleteAsync(id);

                if (result is null)
                    return NotFound($"Line name with ID {id} not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
