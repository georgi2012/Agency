using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.VehicleValidators.Contacts;
using Agency.Data.Models.VehicleValidators.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agency.Data.Models.Vehicles.Models
{
    public class Airplane : Vehicle, IAirplane
    {
        public bool HasFreeFood { get; set; }

        public Airplane(int pasCapacity, decimal price, bool hasFreeFood, IValidator validator)
          : base(pasCapacity, price,
                VehicleType.Air, validator)
        {
            HasFreeFood = hasFreeFood;
        }

        //Poor Man's Dependency Injection
        public Airplane(int pasCapacity, decimal price, bool hasFreeFood)
            : this(pasCapacity, price, hasFreeFood, new AirplaneValidator(pasCapacity, price))
        { }

        public override string ToString()
        {
            return "Airplane ----\n" +
                   $"Passenger capacity: {PassangerCapacity}\n" +
                   $"Price per kilometer: {PricePerKilometer}\n" +
                   $"Vehicle type: {Type.ToString()}\n" +
                   $"Has free food: {HasFreeFood}";
        }
    }
}
