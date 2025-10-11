using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ISerivcesManager
    {
         ITicketPricesServices TicketPricesServices { get; }
        IFaultService FaultService { get;  }
        ICongestionScheduleService CongestionScheduleService { get; }
        ILineNameServices LineNameServices { get; }
        IStationsNameServices StationNameServices { get;  }
        IStation_CoordinatesServices Station_CoordinatesServices { get; }
        IRushTimeServices RushTimeServices { get; }
        ICacheServices CacheServices { get; }

    }
}
