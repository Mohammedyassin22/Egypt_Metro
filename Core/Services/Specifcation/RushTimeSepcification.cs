using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifcation
{
    public class RushTimeSepcification : BaseSpecification<Rush_Times, int>
    {
        private void ApplyIncludes()
        {
            AddInclude(x => x.Station_Name);
            AddInclude(x => x.LineId);
        }
        public RushTimeSepcification(int id) : base(x => x.Id == id)
        {
        }
        public RushTimeSepcification() : base(null)
        {
            ApplyIncludes();
        }
    }
}
