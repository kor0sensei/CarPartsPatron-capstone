using System.Collections.Generic;

namespace CarPartsPatron.Models.ViewModels
{
    public class PartCreateViewModel
    {
        public Part Part { get; set; }
        public List<Car> CarOptions { get; set; }

    }
}
