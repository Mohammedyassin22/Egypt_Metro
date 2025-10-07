using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class CongestionSchedule : BaseEntity<int>
    {
        public int StationNameId { get; set; }

        [ForeignKey(nameof(StationNameId))]
        public Station_Name StationName { get; set; }
        public DateTime ObservationTime { get; set; }
        public CongestionLevels CongestionLevel { get; set; }
        public string Notes { get; set; }
    }

}
