using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Core.Contracts
{
    public interface IJourneyService
    {
        Task CreateJourneyAsync(string startLoc, string dest, int dist, IVehicle veh);
        Task<List<IJourney>> GetJourneyAsync();
        Task<IJourney> GetJourneyWithIdAsync(Guid id);
        Task RemoveJourneyAsync(Guid id);
    }
}
