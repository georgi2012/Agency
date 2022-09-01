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
    public class TrainService :ITrainService
    {
        private readonly AgencyDBContext _dbContext;

        public TrainService(AgencyDBContext db)
        {
            this._dbContext = db;
        }

        public virtual async Task CreateTrainAsync(int capacity, decimal price, int carts)
        {
            await _dbContext.Vehicles.AddAsync(new Train(capacity, carts, price));
            await _dbContext.SaveChangesAsync();
        }


    }
}
