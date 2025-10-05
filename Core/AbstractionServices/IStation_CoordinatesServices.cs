using Domain.Modules;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IStation_CoordinatesServices
    {
        Task<Station_CoordinatesDto> GetCoordinatesByStationName(string stationName);
    }
}
