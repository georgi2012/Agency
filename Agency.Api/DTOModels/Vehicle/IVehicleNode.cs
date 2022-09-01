using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.Api.DTOModels.Vehicle
{
    public interface IVehicleNode
    {
        string AdditionalInfo { get; set; }
        int PassangerCapacity { get; set; }
        decimal PricePerKilometer { get; set; }
        string TransportType { get; set; }
        int VehicleID { get; }
        string VehicleModel { get; set; }

        Task<VehicleNode> MakeNodeFromVehicle(IVehicle veh);
        Task<List<VehicleNode>> MakeNodeListFromVehiclesList(List<IVehicle> vehs);
    }
}