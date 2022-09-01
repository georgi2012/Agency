using Agency.Core.Contracts;
using Agency.Data.Models.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;
//using Agency.Data.Model;

namespace Agency.Core.Services.VehicleServices
{
    public class AirplaneService :IAirplaneService
    {
       private readonly AgencyDBContext _dbContext;

        public AirplaneService(AgencyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual  async Task CreateAirplaneAsync(int capacity, decimal price, bool hasFreeFood)
        {
            await _dbContext.Vehicles.AddAsync(new Airplane(capacity, price, hasFreeFood));
            await _dbContext.SaveChangesAsync();
        }

    }
}
