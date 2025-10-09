using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifcation
{
    public class StationSpecification:BaseSpecification<Ticket_Prices,int>
    {
        private void ApplyIncludes()
        {
            AddInclude(x => x.Price);
            AddInclude(x => x.StationsNumber);
        }
        public StationSpecification(int id):base(x=>x.Id==id)
        {
        }
        public StationSpecification() : base(null)
        {
            ApplyIncludes();
        }
    }
}
