using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Models.Events
{
    public class CreateEventRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime StartOn { get; set; }

        [Required]
        public DateTime EndOn { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }

    }
}
