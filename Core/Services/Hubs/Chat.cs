using Microsoft.AspNet.SignalR;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Hubs
{
    public class Chat(IChatService chatService) :Hub
    {
        public async Task SendMessage(string userMessage)
        {
            var response = await chatService.GetBotResponseAsync(userMessage);

            await Clients.All.SendAsync("ReceiveMessage", userMessage, response);
        }
    }
}
