using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string AppUserId { get; set; }
    }
}
