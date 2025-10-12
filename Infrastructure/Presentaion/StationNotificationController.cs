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
    public class StationNotificationController(ISerivcesManager serivcesManager) :ControllerBase
    {
        [HttpPost("check-proximity")]
        public async Task<IActionResult> CheckProximity([FromBody] UserTripDto userTrip)
        {
            var result = await serivcesManager.StationNotification.CheckProximityAsync(userTrip);
            return Ok(result);
        }
    }
}
