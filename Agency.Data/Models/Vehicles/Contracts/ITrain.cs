namespace Agency.Data.Models.Vehicles.Contracts
{

    public interface ITrain : IVehicle
    {

        int Carts { get; }
    }
}