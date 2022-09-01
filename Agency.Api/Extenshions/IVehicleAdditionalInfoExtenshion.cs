using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;

namespace Agency.Api.Extenshions
{
    public static class IVehicleAdditionalInfoExtenshion
    {
        public static string AdditionalInfo(this IVehicle veh)
        {
            switch (veh)
            {
                case Bus bus:
                    return bus.AdditionalInfo();
                case Airplane airplane:
                    return airplane.AdditionalInfo();
                case Train train:
                    return train.AdditionalInfo();
                case Boat boat:
                    return boat.AdditionalInfo();
                case Vehicle vehicle:
                    return vehicle.AdditionalInfo();
                default: return " - ";
            }
        }
    }
}
