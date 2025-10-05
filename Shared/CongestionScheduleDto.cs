using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CongestionScheduleDto
    {
        public int StationNameId { get; set; }  
        public string congestionLevel { get; set; } 
        public string Notes { get; set; }
    }

}
