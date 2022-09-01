using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Agency.Data.Models.Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        [Key]
        public virtual int VehicleID { get; set; } 

        public virtual VehicleType Type { get; set; }

        public virtual int PassangerCapacity { get; set; }
       
        public virtual decimal PricePerKilometer { get; set; }

        public Vehicle()
        { }

        public Vehicle(int pasCapacity, decimal price,VehicleType type)
        {
            PassangerCapacity = pasCapacity;
            PricePerKilometer = price;
            Type = type;
        }

    }
}
