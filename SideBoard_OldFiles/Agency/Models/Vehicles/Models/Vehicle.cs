using Agency.Models.Vehicles.Contracts;
using Agency.Models.VehicleValidators.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agency.Models.Vehicles.Models
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
        public int PassangerCapacity
        {
            get => _pasCapacity;
            set
            {
                if (_validator != null)
                {
                    //will throw if not valid value
                    _validator.PasCapacity = value;
                }
                _pasCapacity = value;
            }
        }

        public decimal PricePerKilometer
        {
            get => _price;
            set
            {
                if (_validator != null)
                {
                    //will throw if not valid value
                    _validator.Price = value;
                }
                _price = value;
            }
        }


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
