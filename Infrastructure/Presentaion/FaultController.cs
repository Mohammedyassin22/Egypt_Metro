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
    public class FaultController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllFaults")]
        public async Task<IActionResult> GetAllFaultsAsync()
        {
            var faults= await serivcesManager.FaultService.GetAllFaultsAsync();
            return Ok(faults);
        }

        [HttpPost]
        public async Task<IActionResult> AddFaultAsync(FaultDto faultDto)
        {
            try
            {
                var result = await serivcesManager.FaultService.AddFaultAsync(faultDto);
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
