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
    [Route("api/[controller]/[action]")]
    public class TicketPricesController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllPrices")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetAllTicketPricesAsync()
        {
            var prices= await serivcesManager.TicketPricesServices.GetAllTicketPricesAsync();
            return Ok(prices);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> AddTicketPriceAsync( int numStations, int price)
        {
            try
            {
                var result = await serivcesManager.TicketPricesServices.AddTicketPriceAsync(numStations, price);
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

        [HttpPut("UpdatePrice")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> UpdateTicketPriceAsync(int numStations, int newPrice)
        {
            try
            {
                var result = await serivcesManager.TicketPricesServices.UpdateAsync(numStations, newPrice);
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
