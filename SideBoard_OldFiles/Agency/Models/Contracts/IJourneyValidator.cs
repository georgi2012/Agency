﻿using Agency.Models.Vehicles.Contracts;

namespace Agency.Models.Contracts
{
    public interface IJourneyValidator
    {
        bool isValid(string dest, int dist, string startLoc, IVehicle veh);
    }
}