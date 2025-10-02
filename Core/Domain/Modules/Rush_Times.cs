using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules
{
    public enum CongestionLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public class Rush_Times:BaseEntity<int>
    {
        public int LineId { get; set; }
        public Stations_Lines Line { get; set; }
        public DateTime ObservationTime { get; set; } 
        public CongestionLevel congestionLevel { get; set; }
        public string Notes { get; set; }
    }
}
