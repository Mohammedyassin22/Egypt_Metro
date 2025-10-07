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
                if (result is null)
                {
                    return BadRequest("Fault could not be added.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteFault/{id}")]
        public async Task<IActionResult> DeleteFaultAsync(int id)
        {
            try
            {
                var result = await serivcesManager.FaultService.DeleteAsync(id);
                if (result is null)
                {
                    return NotFound($"Fault with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
