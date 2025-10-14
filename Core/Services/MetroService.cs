using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Presistense.Data;
using ServicesAbstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class MetroService(HttpClient httpClient, MetroDbContex metroDb) : IMetroService
    {


        public async Task<List<Station_NameDto>> GetAllStationsAsync()
        {
            return await metroDb.Station_Names
        .Select(s => new Station_NameDto
        {
            StationName = s.StationName,
            Coordinates = $"{s.Coordinates.Latitude}, {s.Coordinates.Longitude}",
            StationsLines = string.Join(", ", s.StationsLines.Select(sl => sl.LineName))
        })
        .ToListAsync();
        }


        public async Task<string> GetGeminiResponseAsync(string userMessage)
        {
            var apiKey = "YOUR_GEMINI_API_KEY";
            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = userMessage }
                }
            }
        }
            };

            var response = await httpClient.PostAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}",
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            );

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
        

        public async Task<MetroTripDto> GetTripBetweenStationsAsync(string fromName, string toName)
        {
            var fromStation = await metroDb.Station_Names.FirstOrDefaultAsync(s => s.StationName == fromName);
            var toStation = await metroDb.Station_Names.FirstOrDefaultAsync(s => s.StationName == toName);

            if (fromStation == null || toStation == null)
                return null;

            var diff = CalculateDistance(fromStation.Coordinates, toStation.Coordinates);


            var ticket = await metroDb.Ticket_Prices
                .OrderBy(t => t.StationsNumber)
                .FirstOrDefaultAsync(t => diff <= t.StationsNumber);

            int price = ticket?.Price ?? 0;

            return new MetroTripDto
            {
                From = fromName,
                To = toName,
                StationsCount = (int)diff,
                TicketPrice = price,
                Description = $"اركب من {fromName} إلى {toName} وعدي على {diff} محطة. سعر التذكرة {price} جنيه."
            };
        }


        private double CalculateDistance(Station_Coordinates from, Station_Coordinates to)
        {
            double R = 6371; 
            double latDiff = (to.Latitude - from.Latitude) * Math.PI / 180;
            double lonDiff = (to.Longitude - from.Longitude) * Math.PI / 180;

            double a = Math.Sin(latDiff / 2) * Math.Sin(latDiff / 2) +
                       Math.Cos(from.Latitude * Math.PI / 180) *
                       Math.Cos(to.Latitude * Math.PI / 180) *
                       Math.Sin(lonDiff / 2) * Math.Sin(lonDiff / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            return distance; 
        }


    }
}
