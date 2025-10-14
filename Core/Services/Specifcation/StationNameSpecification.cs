using Domain.Contracts;
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
        public StationNameSpecification(string? name)
            : base(s => string.IsNullOrEmpty(name) || s.StationName.ToLower().Contains(name.ToLower()))
        {
            AddInclude(s => s.Coordinates);
            AddInclude(s => s.StationsLines);
        }

        public StationNameSpecification()
            : base(null)
        {
            AddInclude(s => s.Coordinates);
            AddInclude(s => s.StationsLines);
        }

        public StationNameSpecification(StationNameSpecificationParameter parameter)
            : base(x => string.IsNullOrEmpty(parameter.Search) || x.StationName.ToLower().Contains(parameter.Search.ToLower()))
        {
            AddInclude(s => s.Coordinates);
            AddInclude(s => s.StationsLines);
        }
    }

}
