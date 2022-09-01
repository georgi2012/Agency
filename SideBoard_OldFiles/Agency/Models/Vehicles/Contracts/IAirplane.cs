namespace Agency.Models.Vehicles.Contracts
{
    public interface IAirplane : IVehicle
    {
        bool HasFreeFood { get; }
    }
}