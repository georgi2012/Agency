using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.VehicleValidators.Contacts;
using Agency.Data.Models.VehicleValidators.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Agency.Data.Models.Vehicles.Models
{
    public class Train : Vehicle, ITrain
    {
        //fields
        private int _carts;

        // properties
        [Range(1000,1000000,ErrorMessage ="Bad range")]
        public int Carts { get; set; }
        //{
        //    get => _carts;
        //    set
        //    {
        //        var trainValidator = base.Validator as ITrainValidator;
        //        if (trainValidator != null)
        //        {
        //            //will throw if invalid
        //            trainValidator.Carts = value;
        //            _carts = value;
        //        }
        //    }
        //}

        public Train(int pasCapacity, int carts, decimal price, ITrainValidator validator)
            : base(pasCapacity, price, VehicleType.Land, validator)
        {
            Carts = carts;
        }

        //Poor Man's Dependency Injection
        public Train(int pasCapacity, int carts, decimal price)
           : this(pasCapacity, carts, price, new TrainValidator(pasCapacity, price, carts))
        { }

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
