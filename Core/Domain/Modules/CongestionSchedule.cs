using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules
{
    public class CongestionSchedule:BaseEntity<int>
    {
        public int LineId { get; set; }
        public Line_Name Line { get; set; }
        public DateTime ObservationTime { get; set; }
        public int CongestionLevel { get; set; }
        public string Notes { get; set; }
    }
}
