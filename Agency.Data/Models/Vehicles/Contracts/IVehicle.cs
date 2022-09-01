using System.ComponentModel.DataAnnotations;

namespace Agency.Data.Models.Vehicles.Contracts
{
    public interface IVehicle
    {
        int VehicleID { get; }
        int PassangerCapacity { get; set; }
        VehicleType Type { get; set; }
        decimal PricePerKilometer { get; set; }
    }
}