using SupportWheelOfFate.Core;
using System;
using System.Collections.Generic;

namespace SupportWheelOfFate.Web.Models
{
    public class SupportViewModel
    {
        public SupportViewModel()
        {
            this.AssignedEngineers = new List<Engineer>();
        }

        public List<Engineer> AssignedEngineers { get; set; }

        public DateTime SupportDate { get; set; }
    }
}
