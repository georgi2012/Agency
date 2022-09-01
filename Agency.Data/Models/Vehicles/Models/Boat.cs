using Agency.Data.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Data.Models.Vehicles.Models
{
    public class Boat : Vehicle, IBoat
    {
        public virtual bool OffersWaterSports { get;  set; }

        public Boat()
        {

        }
        public Boat(int pasCapacity, decimal price, bool offersWaterSports)
            : base(pasCapacity, price,VehicleType.Sea)
        {
            OffersWaterSports = offersWaterSports;
        }


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
