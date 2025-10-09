using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifcation
{
    public class LineNameSpecification : BaseSpecification<Line_Name, int>
    {
        private void ApplyIncludes()
        {
            AddInclude(x => x.LineName);
            AddInclude(x => x.ColorCode);
        }
        public LineNameSpecification(string lineName)
           : base(x => x.LineName == lineName)
        {
        }
        public LineNameSpecification() : base(null)
        {
            ApplyIncludes();
        }
    }
}