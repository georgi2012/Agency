using Agency.Data.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Agency.Data.Models.Vehicles.Models
{
    public class Train : Vehicle, ITrain
    {
        public virtual int Carts { get; set; }

        public Train()
        {

        }
        public Train(int pasCapacity, int carts, decimal price)
            : base(pasCapacity, price, VehicleType.Land)
        {
            Carts = carts;
        }

    
        public override string ToString()
        {
            return "Train ----\n" +
                   $"Passenger capacity: {PassangerCapacity}\n" +
                   $"Price per kilometer: {PricePerKilometer}\n" +
                   $"Vehicle type: {Type.ToString()}\n" +
                   $"Carts amount: {Carts}";
        }
    }
}
