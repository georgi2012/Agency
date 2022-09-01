using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.VehicleValidators.Contacts;
using Agency.Data.Models.VehicleValidators.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agency.Data.Models.Vehicles.Models
{
    public class Bus : Vehicle, IBus
    {
        public Bus(int pasCapacity, decimal price, IValidator validator)
            : base(pasCapacity, price, VehicleType.Land, validator)
        {}

        //Poor Man's Dependency Injection
        public Bus(int pasCapacity, decimal price)
         : this(pasCapacity, price, new BusValidator(pasCapacity, price))
        { }

        public override string ToString()
        {
            return "Bus ----\n" +
                   $"Passenger capacity: {PassangerCapacity}\n" +
                   $"Price per kilometer: {PricePerKilometer}\n" +
                   $"Vehicle type: {Type.ToString()}";
        }
    }
}
