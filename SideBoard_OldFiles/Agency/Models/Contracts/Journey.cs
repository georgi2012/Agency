using Agency.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agency.Models.Contracts
{
    public class Journey : IJourney
    {
        private readonly Guid ID;
        public Guid JourneyID { get => ID; }
        public string Destination { get; private set; }
        public string StartLocation { get; private set; }
        public int Distance { get; private set; }
        public IVehicle Vehicle { get; private set; }
        private readonly IJourneyValidator journeyValidator;

        public Journey(string dest, int dist, string startLoc,
            IVehicle veh, IJourneyValidator journeyValidator)
        {
            journeyValidator.isValid(dest, dist, startLoc, veh);
            this.journeyValidator = journeyValidator;
            StartLocation = startLoc;
            Destination = dest;
            Distance = dist;
            Vehicle = veh;
            ID = Guid.NewGuid();
        }

        public Journey(string dest, int dist, string startLoc, IVehicle veh)
            : this(dest, dist, startLoc, veh, new JourneyValidator())
        { }

        public decimal CalculateTravelCosts()
        {
            return Vehicle.PricePerKilometer * Distance;
        }

        public override string ToString()
        {
            return "Journey ----\n" +
                   $"Start location: {StartLocation}\n" +
                   $"Destination: {Destination}\n" +
                   $"Distance: {Distance}\n" +
                   $"Vehicle type: {Vehicle.Type}\n" +
                   $"Travel costs: {CalculateTravelCosts()}";
        }
    }
}
