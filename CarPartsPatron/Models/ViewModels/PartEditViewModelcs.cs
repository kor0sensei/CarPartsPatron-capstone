using System.Collections.Generic;

namespace CarPartsPatron.Models.ViewModels
{
    public class PartEditViewModel
    {
        public Part Part { get; set; }
        public List<Car> CarOptions { get; set; }

    }
}
