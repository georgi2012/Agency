using Agency.Data.Models.Vehicles.Contracts;
using System;

namespace Agency.Data.Models.Contracts
{
    public interface IJourney
    {

        Guid JourneyID { get; }
        string Destination { get; }

        int Distance { get; }

        string StartLocation { get;}

        IVehicle Vehicle { get; }

        decimal CalculateTravelCosts();
    }
}