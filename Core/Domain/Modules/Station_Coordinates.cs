using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Domain.Modules
{
    public class Station_Coordinates:BaseEntity<int>
    {
        public double Latitude { get; set; } 
        public double Longitude { get; set; }

        public int StationId { get; set; }
        public Station_Name Station { get; set; }
    }
}
