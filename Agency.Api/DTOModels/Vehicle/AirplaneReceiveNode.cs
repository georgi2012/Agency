using System.ComponentModel.DataAnnotations;

namespace Agency.Api.DTOModels.Vehicle
{
    public class AirplaneReceiveNode
    {
        [Range(1,800,ErrorMessage ="Vehicle capacity can't exceed 800 or be less than 1")]
        public int PassangerCapacity { get; set; }
        [Range(0.1, 5, ErrorMessage = "Vehicle price per km can't exceed 5 or (unfortunately) cost less than 0.1")]
        public decimal PricePerKilometer { get; set; }
        public bool HasFreeFood { get; set; }
        public int ID { get; set; }
    }
}
