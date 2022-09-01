using Agency.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Models.Contracts
{
    public class JourneyValidator : IJourneyValidator
    {
        public JourneyValidator()
        { }

        public bool isValid(string dest, int dist, string startLoc, IVehicle veh)
        {
            if (startLoc.Length < 5 || startLoc.Length > 25)
            {
                throw new ArgumentException("The StartingLocation's length cannot be less than 5 or more than 25 symbols long.");
            }
            if (dest.Length < 5 || dest.Length > 25)
            {
                throw new ArgumentException("The Destination's length cannot be less than 5 or more than 25 symbols long.");
            }
            if (dist < 1)// || dist > 5000)
            {
                throw new ArgumentException("The Distance cannot be less than 5 or more than 5000 kilometers.");
            }
            if (veh == null)
            {
                throw new ArithmeticException("Vehicle not set.");
            }
            return true;
        }
    }
}
