using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarPartsPatron.Models
{
    public class Part
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string  Brand { get; set; }
        public string PartType { get; set; }
        public int? Price { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? DateInstalled { get; set; }
    }
}
