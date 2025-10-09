using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifcation
{
    public class FaultsSpecification : BaseSpecification<Faults, int>
    {
        private void ApplyIncludes()
        {
            AddInclude(x => x.StartTime);
            AddInclude(x => x.EndTime);
            AddInclude(x => x.Description);
        }
        public FaultsSpecification()
            : base(null)
        {
            AddInclude(f => f.Line);
        }
        public FaultsSpecification(int id)
            : base(f => f.Id == id)
        {
            AddInclude(f => f.Line);
        }
    }
}
