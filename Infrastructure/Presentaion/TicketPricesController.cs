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
        public async Task<IActionResult> AddTicketPriceAsync([FromBody] int numStations, int price)
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
    }
}
