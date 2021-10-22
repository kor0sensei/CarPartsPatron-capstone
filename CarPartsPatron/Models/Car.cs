using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarPartsPatron.Models
{
    public class Car
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        public string Submodel { get; set; }
        public string Engine { get; set; }
        public string Drivetrain { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        [Required] 
        public string PhotoUrl { get; set; }
    }
}
