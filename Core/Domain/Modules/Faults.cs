using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Domain.Modules
{
    public class Faults:BaseEntity<int>
    {
        public int LineId { get; set; }
        public Line_Name Line { get; set; }
        public string Title { get; set; }

        public int? StationId { get; set; }
        public Station_Name Station { get; set; }

        public DateTime StartTime { get; set; } 

        public DateTime? EndTime { get; set; } 

        public string Description { get; set; } 
        public string Status { get; set; } = "Reported"; 
    }
}
