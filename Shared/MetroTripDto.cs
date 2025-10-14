using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class MetroTripDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public int StationsCount { get; set; }
        public int TicketPrice { get; set; }
        public string Description { get; set; }
    }
}
