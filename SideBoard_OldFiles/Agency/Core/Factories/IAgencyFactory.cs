using Agency.Models.Contracts;
using Agency.Models.Vehicles.Contracts;

namespace Agency.Core.Contracts
{
    public interface IAgencyFactory
    {
        IBus CreateBus(int passengerCapacity, decimal pricePerKilometer);
        
        ITrain CreateTrain(int passengerCapacity, decimal pricePerKilometer, int carts);

        IJourney CreateJourney(string startingLocation, string destination, int distance, IVehicle vehicle);

        IBoat CreateBoat(int passengerCapacity, decimal pricePerKilometer, bool hasWaterSports);

        ITicket CreateTicket(IJourney journey, decimal administrativeCosts);

        IAirplane CreateAirplane(int passengerCapacity, decimal pricePerKilometer, bool hasFreeFood);
    }
}
