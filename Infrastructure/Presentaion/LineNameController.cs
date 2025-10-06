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
    public class LineNameController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpGet("GetAllLineNames")]
        public async Task<IActionResult> GetAllLineNamesAsync()
        {
            var lines= await serivcesManager.LineNameServices.GetAllStationsAsync();
            return Ok(lines);
        }

        [HttpGet("GetLineByName/{lineName}")]
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
        public async Task<IActionResult> AddLineNameAsync([FromBody] string lineName)
        {
            try
            {
                var line = await serivcesManager.LineNameServices.GetLineNameAsync(lineName);
                if (line == null)
                    return NotFound();

                return Ok(line);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
