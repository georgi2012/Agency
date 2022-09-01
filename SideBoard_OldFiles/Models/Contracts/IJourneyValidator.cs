using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.Data.Models.Contracts
{
    public interface IJourneyValidator
    {
        bool isValid(string dest, int dist, string startLoc, IVehicle veh);
    }
}