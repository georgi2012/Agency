using System.ComponentModel.DataAnnotations;

namespace Agency.Api.DTOModels.Vehicle
{
    public class TrainReceiveNode
    {
        [Range(30, 150, ErrorMessage = "Train capacity can't exceed 150 or be less than 30")]
        public int PassangerCapacity { get; set; }
        [Range(0.1, 5, ErrorMessage = "Vehicle price per km can't exceed 5 or (unfortunately) cost less than 0.1")]
        public decimal PricePerKilometer { get; set; }
        [Range(1, 15, ErrorMessage = "Train carts can't exceed 15 or be less than 1")]
        public int Carts { get; set; }
        public int ID { get; set; }
    }
}
