﻿using Newtonsoft.Json;

namespace SupportWheelOfFate.Core
{
    public class Engineer
    {
        public Engineer()
        {
            UpdateEngineer = false;
        }

        [JsonProperty(PropertyName = "engineerId")]
        public string EngineerId { get; set; }

        [JsonProperty(PropertyName = "engineerName")]
        public string EngineerName { get; set; }

        [JsonProperty(PropertyName = "engineerHandle")]
        public string EngineerHandle { get; set; }

        [JsonProperty(PropertyName = "dateLastShift")]
        public string DateLastShift { get; set; }

        [JsonProperty(PropertyName = "timeLastShift")]
        public string TimeLastShift { get; set; }

        [JsonIgnore]
        public bool UpdateEngineer { get; set; }
    }
}
