using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IStationsNameServices
    {
        Task<string> GetStationNameAsync(string name);
        Task<IEnumerable<Station_NameDto>> GetAllStationsAsync();
        Task<Station_NameDto> AddStationWithCoordinatesAsync(string stationName, CancellationToken cancellationToken = default);

        }
}
