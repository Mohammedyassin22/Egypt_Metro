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
    public class ChatController(ISerivcesManager serivcesManager):ControllerBase
    {
        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] string userMessage)
        {
            var response = await serivcesManager.ChatService.GetBotResponseAsync(userMessage);
            return Ok(new { reply = response });
        }
    }
}
