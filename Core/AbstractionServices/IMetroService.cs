using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IMetroService
    {
        Task<List<Station_NameDto>> GetAllStationsAsync();
        Task<MetroTripDto> GetTripBetweenStationsAsync(string fromName, string toName);
        Task<string> GetGeminiResponseAsync(string userMessage);
    }

}
