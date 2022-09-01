
namespace Agency.Data.Models.Vehicles.Contracts
{
    public interface IAirplane : IVehicle
    {
        bool HasFreeFood { get; }
    }
}