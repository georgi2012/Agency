using Agency.Core.Contracts;
using Agency.Data.Models.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;

namespace Agency.Core.Services.VehicleServices
{
    public class BoatService : IBoatService
    {
        private readonly AgencyDBContext _dbContext;

        public BoatService(AgencyDBContext db)
        {
            this._dbContext = db;
        }
        public virtual  async Task CreateBoatAsync(int capacity, decimal price, bool hasWaterSports)
        {
            await _dbContext.Vehicles.AddAsync(new Boat(capacity, price, hasWaterSports));
            await _dbContext.SaveChangesAsync();

        }
    }
}
