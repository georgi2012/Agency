using Agency.Models.Vehicles.Contracts;
using Agency.Models.VehicleValidators.Contacts;
using Agency.Models.VehicleValidators.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Models.Vehicles.Models
{
    public class Boat : Vehicle, IBoat
    {
        public bool OffersWaterSports { get;  set; }

        public Boat(int pasCapacity, decimal price, bool offersWaterSports,IValidator validator)
            : base(pasCapacity, price,
                  VehicleType.Sea, validator)
        {
            OffersWaterSports = offersWaterSports;
        }

        //Poor Man's Dependency Injection
        public Boat(int pasCapacity, decimal price, bool offersWaterSports)
          : this(pasCapacity, price, offersWaterSports, new BoatValidator(pasCapacity, price))
        { }

        public override string ToString()
        {
            return "Boat ----\n" +
                   $"Passenger capacity: {PassangerCapacity}\n" +
                   $"Price per kilometer: {PricePerKilometer}\n" +
                   $"Vehicle type: {Type.ToString()}\n" +
                   $"Offers water sports: {OffersWaterSports}";
        }
    }
}
