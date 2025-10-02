using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules
{
    public class Chatbot:BaseEntity<int>
    {
        public string TriggerPhrase { get; set; } 
        public string ResponseText { get; set; } 
        public string Category { get; set; } 

    }
}
