using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class UserTripDto
    {
        public int UserId { get; set; }
        public string TargetStationName { get; set; }
        public LocationDto CurrentLocation{ get; set; }
    }
}
