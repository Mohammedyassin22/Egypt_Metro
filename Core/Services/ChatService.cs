using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ChatService(MetroService metroService) : IChatService
    {
        public async Task<string> GetBotResponseAsync(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
                return "من فضلك اكتب سؤالك علشان أقدر أساعدك 😊";

            userMessage = userMessage.ToLower();

            var stations = await metroService.GetAllStationsAsync();
            var from = stations.FirstOrDefault(s => userMessage.Contains(s.StationName.ToLower()));
            var to = stations.LastOrDefault(s => userMessage.Contains(s.StationName.ToLower()) && s != from);

            if (from != null && to != null)
            {
                var trip = await metroService.GetTripBetweenStationsAsync(from.StationName, to.StationName);
                if (trip != null)
                    return trip.Description;
            }

            return await metroService.GetGeminiResponseAsync(userMessage);
        }
    }
}
