using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.VehicleValidators.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Agency.Data.Models.Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        private static int ID = 1;
        //
        private readonly int _vehicleID;
        private readonly IValidator _validator;
        private int _pasCapacity;
        private decimal _price;
        public int VehicleID { get => _vehicleID; }

        public VehicleType Type { get; set; }

        public IValidator Validator { get => _validator; }
        [Range(100,1000,ErrorMessage ="Range failed for vehicle capacity")]
        public int PassangerCapacity { get; set; }
        //{
        //    get => _pasCapacity;
        //    set
        //    {
        //        if (_validator != null)
        //        {
        //            //will throw if not valid value
        //            _validator.PasCapacity = value;
        //        }
        //        _pasCapacity = value;
        //    }
        //}
        [Range(100, 1000, ErrorMessage = "Range failed for vehicle price")]
        public decimal PricePerKilometer { get; set; }
        //{
        //    get => _price;
        //    set
        //    {
        //        if (_validator != null)
        //        {
        //            //will throw if not valid value
        //            _validator.Price = value;
        //        }
        //        _price = value;
        //    }
        //}


        public Vehicle(int pasCapacity, decimal price,
            VehicleType type, IValidator validator)
        {
            PassangerCapacity = pasCapacity;
            PricePerKilometer = price;
            Type = type;
            validator.isValid();
            this._validator = validator;

            _vehicleID = ID++;
        }



    }
}
