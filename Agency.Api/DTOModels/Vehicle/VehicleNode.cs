using Agency.Api.Extenshions;
using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.Api.DTOModels.Vehicle
{

    public class VehicleNode : IVehicleNode
    {
        public VehicleNode()
        {

        }

        public VehicleNode(int vehicleID, int passangerCapacity, string transportType,
            decimal pricePerKilometer, string vehicleModel, string additionalInfo)
        {
            VehicleID = vehicleID;
            PassangerCapacity = passangerCapacity;
            TransportType = transportType;
            PricePerKilometer = pricePerKilometer;
            VehicleModel = vehicleModel;
            AdditionalInfo = additionalInfo;
        }

        public int VehicleID { get; }
        public int PassangerCapacity { get; set; }
        public string TransportType { get; set; }
        public decimal PricePerKilometer { get; set; }
        public string VehicleModel { get; set; }

        public string AdditionalInfo { get; set; }

        virtual public async Task<VehicleNode> MakeNodeFromVehicle(IVehicle veh)
        {
            string transportType = veh.Type.ToString();
            string vehicleType = veh.GetType().Name;
            return new VehicleNode(veh.VehicleID, veh.PassangerCapacity, transportType,
                veh.PricePerKilometer, vehicleType, veh.AdditionalInfo());

        }

        public virtual async Task<List<VehicleNode>> MakeNodeListFromVehiclesList(List<IVehicle> vehs)
        {
            List<VehicleNode> list = new();
            foreach (var item in vehs)
            {
                list.Add(await MakeNodeFromVehicle(item));
            }
            return list;
        }

    }
}
