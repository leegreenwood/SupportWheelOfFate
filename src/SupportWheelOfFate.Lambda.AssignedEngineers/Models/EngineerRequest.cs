namespace SupportWheelOfFate.Lambda.AssignedEngineers.Models
{
    public class EngineerRequest
    {
        public EngineerRequest()
        {
            UpdateEngineers = true;
        }

        public string SupportDate { get; set; }

        public bool UpdateEngineers { get; set; }
    }
}
