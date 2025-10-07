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
    public class TicketPricesController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllPrices")]
        public async Task<IActionResult> GetAllTicketPricesAsync()
        {
            var prices= await serivcesManager.TicketPricesServices.GetAllTicketPricesAsync();
            return Ok(prices);
        }

        [HttpPost]
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
