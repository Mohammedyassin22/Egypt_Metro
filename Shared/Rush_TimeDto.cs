using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Rush_TimeDto
    {
        public string LineName { get; set; }
        public string StationName { get; set; }
        public DateTime ObservationTime { get; set; }
        public string congestionLevel { get; set; }
        public string Notes { get; set; }
    }
}
