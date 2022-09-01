using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using System;

namespace Agency.Data.Models.Contracts
{
    public interface IJourney
    {

        Guid JourneyID { get; }
        string Destination { get; }

        int Distance { get; }

        string StartLocation { get;}

        int VehicleID { get; }

        Vehicle Vehicle { get; set; }

        decimal CalculateTravelCosts();
    }
}