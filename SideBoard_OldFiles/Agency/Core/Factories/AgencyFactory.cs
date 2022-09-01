using System;
using Agency.Core.Contracts;
using Agency.Models.Contracts;
using Agency.Models.Vehicles.Contracts;
using Agency.Models.Vehicles.Models;

namespace Agency.Core.Factories
{
    public class AgencyFactory : IAgencyFactory
    {

        public AgencyFactory()
        {}

        public IBus CreateBus(int passengerCapacity, decimal pricePerKilometer)
        {
            return new Bus(passengerCapacity, pricePerKilometer);
        }

        public IAirplane CreateAirplane(int passengerCapacity, decimal pricePerKilometer, bool hasFreeFood)
        {
            return new Airplane(passengerCapacity, pricePerKilometer, hasFreeFood);
        }

        public ITrain CreateTrain(int passengerCapacity, decimal pricePerKilometer, int carts)
        {
            return new Train(passengerCapacity, carts, pricePerKilometer);
        }
        public IBoat CreateBoat(int passengerCapacity, decimal pricePerKilometer, bool hasWaterSports)
        {
            return new Boat(passengerCapacity, pricePerKilometer, hasWaterSports);
        }
        public IJourney CreateJourney(string startLocation, string destination, int distance, IVehicle vehicle)
        {
            return new Journey(destination, distance, startLocation, vehicle);
        }

        public ITicket CreateTicket(IJourney journey, decimal administrativeCosts)
        {
            return new Ticket(administrativeCosts, journey);
        }
    }
}
