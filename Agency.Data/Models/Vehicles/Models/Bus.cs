using Agency.Data.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agency.Data.Models.Vehicles.Models
{
    public class Bus : Vehicle, IBus
    {
        public Bus(int pasCapacity, decimal price)
            : base(pasCapacity, price, VehicleType.Land)
        {}

        public Bus()
        {

        }
        public override string ToString()
        {
            return "Bus ----\n" +
                   $"Passenger capacity: {PassangerCapacity}\n" +
                   $"Price per kilometer: {PricePerKilometer}\n" +
                   $"Vehicle type: {Type.ToString()}";
        }
    }
}
