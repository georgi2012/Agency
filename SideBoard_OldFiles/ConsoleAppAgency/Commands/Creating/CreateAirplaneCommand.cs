using System;
using System.Collections.Generic;
using Agency.Commands.Contracts;
using Agency.Core.Contracts;

namespace Traveller.Commands.Creating
{
    // TODO
    public class CreateAirplaneCommand : ICommand
    {
        private readonly IAgencyFactory factory;
        private readonly IEngine engine;

        public CreateAirplaneCommand(IAgencyFactory factory, IEngine engine)
        {
            this.factory = factory;
            this.engine = engine;
        }

        public string Execute(IList<string> parameters)
        {
            int passengerCapacity;
            decimal pricePerKilometer;
            bool hasFreeFood;

            try
            {
                passengerCapacity = int.Parse(parameters[0]);
                pricePerKilometer = decimal.Parse(parameters[1]);
                hasFreeFood = bool.Parse(parameters[2]);
                //[HasFreeFood]
            }
            catch
            {
                throw new ArgumentException("Failed to parse CreateAirplane command parameters.");
            }

            var plane = this.factory.CreateAirplane(passengerCapacity, pricePerKilometer,hasFreeFood);
            this.engine.Vehicles.Add(plane);

            return $"Vehicle with ID {engine.Vehicles.Count - 1} was created.";
        }
    }
}
