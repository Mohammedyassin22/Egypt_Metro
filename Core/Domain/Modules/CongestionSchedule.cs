using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules
{
    public enum CongestionLevels
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public class CongestionSchedule:BaseEntity<int>
    {
        public int StationName { get; set; }
        public Station_Name Name { get; set; }
        public DateTime ObservationTime { get; set; }
        public CongestionLevels congestionLevel { get; set; }
        public string Notes { get; set; }
    }
}
