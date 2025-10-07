using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Domain.Modules
{
    public class Stations_Lines:BaseEntity<int>
    {
        public int Id { get; set; }
        public string LineName { get; set; }

        public int StationNameId { get; set; }
        public Station_Name Station { get; set; }

        
        public int LineId { get; set; }  
        public Line_Name Line { get; set; }  
    }
}
