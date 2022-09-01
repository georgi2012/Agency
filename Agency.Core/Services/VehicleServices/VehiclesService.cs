using Agency.Data.DB;
using Agency.Core.Contracts;
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.EntityFrameworkCore;

namespace Agency.Core.Services.VehicleServices
{
    public class VehicleService : IVehicleService
    {
        readonly IJourneyService journeyService;
        private readonly AgencyDBContext _dbContext;

        public VehicleService(AgencyDBContext db, IJourneyService journeyService)
        {
            this._dbContext = db;
            this.journeyService = journeyService;
        }

        public virtual async Task<List<IVehicle>> GetVehiclesAsync() => 
            await  _dbContext.Vehicles.ToListAsync<IVehicle>();

        public virtual async Task<IVehicle> GetVehicleWithIdAsync(int id) =>
            await _dbContext.Vehicles.FirstOrDefaultAsync(el => el.VehicleID == id);


        public virtual async Task RemoveVehicleAsync(int id)
        {
            var journeys = await journeyService.GetJourneyAsync();
            List<IJourney> jouneysToRemove = new List<IJourney>();
            foreach (var journey in journeys)
            {
                if (journey.VehicleID == id)
                {
                    jouneysToRemove.Add(journey);
                }
            }
            foreach (var journey in jouneysToRemove)
            {
                await journeyService.RemoveJourneyAsync(journey.JourneyID);
            }

            _dbContext.Vehicles.Remove((Vehicle)(await GetVehicleWithIdAsync(id)));
            await _dbContext.SaveChangesAsync();
        }




    }
}
