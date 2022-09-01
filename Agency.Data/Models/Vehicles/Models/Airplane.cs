using Agency.Data.Models.Vehicles.Contracts;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agency.Data.Models.Vehicles.Models
{
    public class Airplane : Vehicle, IAirplane
    {
        public virtual bool HasFreeFood { get; set; }

        public Airplane()
        {

        }

        public Airplane(int pasCapacity, decimal price, bool hasFreeFood)
          : base(pasCapacity, price, VehicleType.Air)
        {
            HasFreeFood = hasFreeFood;
        }


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
