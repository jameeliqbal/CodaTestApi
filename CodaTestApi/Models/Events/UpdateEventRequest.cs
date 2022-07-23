using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Models.Events
{
    public class UpdateEventRequest
    {
        public string Title { get; set; }
        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

    }
}
