using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Data.Models.Vehicles.Contracts
{
    public interface IBoat : IVehicle
    {
        bool OffersWaterSports { get; }
    }
}
