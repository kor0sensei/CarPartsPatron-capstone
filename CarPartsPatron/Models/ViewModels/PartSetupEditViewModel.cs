using System.Collections.Generic;

namespace CarPartsPatron.Models.ViewModels
{
    public class PartSetupEditViewModel
    {
        public PartSetup PartSetup { get; set; }
        public List<Part> PartOptions { get; set; }

    }
}
