using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarPartsPatron.Models
{
    public class PartSetup
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public Part Part { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string SetupNote { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}