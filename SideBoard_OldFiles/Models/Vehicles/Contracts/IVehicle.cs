using System.ComponentModel.DataAnnotations;

namespace Agency.Data.Models.Vehicles.Contracts
{
    public interface IVehicle
    {
        int VehicleID { get; }
        [Range(100, 1000, ErrorMessage = "Range failed for vehicle capacity")]
        int PassangerCapacity { get; set; }
        VehicleType Type { get; set; }
        [Range(100, 1000, ErrorMessage = "Range failed for vehicle price")]
        decimal PricePerKilometer { get; set; }
    }
}