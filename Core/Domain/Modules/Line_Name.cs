using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Domain.Modules
{
    public class Line_Name:BaseEntity<int>
    {
        public string LineName { get; set; } 
        public string ColorCode { get; set; } 
        public ICollection<Stations_Lines> StationsLines { get; set; }
        public ICollection<Station_Coordinates> CongestionSchedules { get; set; } 
        public ICollection<Faults> Faults { get; set; }
    }
}
