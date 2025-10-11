using Domain.Modules;
using Shared;
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
            AddInclude(s => s.StationsLines);
            AddInclude(s => s.StationsLines.Select(sl => sl.Line));
        }

       
        public StationNameSpecification(string name)
            : base(s => s.StationName == name)
        {
            ApplyIncludes();
        }
        public StationNameSpecification()
            : base(null)
        {
            ApplyIncludes();
        }

        public StationNameSpecification(StationNameSpecificationParameter parameter): 
            base(x => (string.IsNullOrEmpty(parameter.Search) || x.StationName.ToLower().Contains(parameter.Search.ToLower())))
        {
            ApplyIncludes();
        }
    }
    }
