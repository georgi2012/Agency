using System.ComponentModel.DataAnnotations;

namespace Agency.Api.DTOModels.Journey
{
    public class JourneyReceiveNode
    {
        public int VehicleID { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9-\s]{2,30}$",
            ErrorMessage = "Invalid destination name.")]
        public string Destination { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9-\s]{2,30}$",
           ErrorMessage = "Invalid start location name.")]
        public string StartLocation { get; set; }
        [Range(1,int.MaxValue, ErrorMessage = "Invalid distance. Should be positive integer")]
        public int Distance { get; set; }
    }
}
