using System.Collections.Generic;

namespace CarPartsPatron.Models.ViewModels
{
    public class PartSetupCreateViewModel
    {
        public PartSetup PartSetup { get; set; }
        public List<Part> PartOptions { get; set; }

    }
}
