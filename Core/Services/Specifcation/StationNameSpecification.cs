using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifcation
{
    public class StationNameSpecification : BaseSpecification<Station_Name, int>
    {
        private void ApplyIncludes()
        {
            AddInclude(x => x.StationName);
            AddInclude(x => x.StationsLines);
        }
        public StationNameSpecification(string name)
           : base(s => s.StationName == name)
        {
            AddInclude(s => s.StationsLines);
        }
        public StationNameSpecification() : base(null)
        {
            ApplyIncludes();
        }
    }
    }
