using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class FaultDto
    {
        public int LineId { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
    }

}
