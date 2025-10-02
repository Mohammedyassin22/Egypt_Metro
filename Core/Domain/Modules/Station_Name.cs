using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules
{
    public class Station_Name:BaseEntity<int>
    {
        public string StationName { get; set; } 
        public Station_Coordinates Coordinates { get; set; }
        public ICollection<Stations_Lines> StationsLines { get; set; }
    }
}
