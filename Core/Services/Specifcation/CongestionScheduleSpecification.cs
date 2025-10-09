using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifcation
{
    public class CongestionScheduleSpecification : BaseSpecification<CongestionSchedule, int>
    {
        private void ApplyIncludes()
        {
            AddInclude(x => x.Notes);
            AddInclude(x => x.StationName);
        }
        public CongestionScheduleSpecification(int id) : base(x => x.Id == id)
        {
        }
        public CongestionScheduleSpecification() : base(null)
        {
            ApplyIncludes();
        }
    }
    }
