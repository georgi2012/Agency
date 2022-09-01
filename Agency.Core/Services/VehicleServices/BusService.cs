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
    public class BusService :IBusService
    {
        private readonly AgencyDBContext _dbContext;

        public BusService(AgencyDBContext db)
        {
            this._dbContext = db;
        }

        public virtual async Task CreateBusAsync(int capacity, decimal price)
        {
            await _dbContext.Vehicles.AddAsync(new Bus(capacity, price));
            await _dbContext.SaveChangesAsync();
        }

    }
}
