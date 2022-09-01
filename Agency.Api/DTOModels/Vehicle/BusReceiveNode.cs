using System.ComponentModel.DataAnnotations;

namespace Agency.Api.DTOModels.Vehicle
{
    public class BusReceiveNode
    {
        [Range(10, 50, ErrorMessage = "Bus capacity can't exceed 50 or be less than 10")]
        public int PassangerCapacity { get; set; }
        [Range(0.1, 5, ErrorMessage = "Vehicle price per km can't exceed 5 or (unfortunately) cost less than 0.1")]
        public decimal PricePerKilometer { get; set; }
        public int ID { get; set; }
    }
}
