using Agency.Data.DB;
using Agency.Core.Contracts;
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Agency.Data.Models.Vehicles.Models;

namespace Agency.Core.Services.JourneyServices
{
    public class JourneyService : IJourneyService
    {
        readonly AgencyDBContext _dbContext;
        readonly ITicketService ticketService;

        public JourneyService(AgencyDBContext db, ITicketService ticketService)
        {
            this._dbContext = db;
            this.ticketService = ticketService;
        }

        public virtual async Task<List<IJourney>> GetJourneyAsync() =>
            await _dbContext.Journeys.ToListAsync<IJourney>();

        public virtual async Task<IJourney> GetJourneyWithIdAsync(Guid id)
            => await _dbContext.Journeys.FirstOrDefaultAsync(el => el.JourneyID == id);

        public virtual async Task CreateJourneyAsync(string startLoc, string dest, int dist,
            IVehicle veh)
        {
            _dbContext.Journeys.Add(new Journey(dest, dist, startLoc, (Vehicle)veh));
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveJourneyAsync(Guid id)
        {
            var journey = (Journey)await GetJourneyWithIdAsync(id);
            if (journey == null)
            {
                throw new Exception("Journey with such ID could not be found");
            }
            //Remove all tickets that use that journey as well.
            var tickets = await ticketService.GetTicketsAsync();

            List<ITicket> ticketsToRemove = new List<ITicket>();
            foreach (var ticket in tickets)
            {
                if (ticket.JourneyID == id)
                {
                    ticketsToRemove.Add(ticket);
                }
            }
            foreach (var ticket in ticketsToRemove)
            {
                await ticketService.RemoveTicketAsync(ticket.TicketID);
            }
           
            _dbContext.Journeys.Remove(journey);
            await _dbContext.SaveChangesAsync();
        }


    }
}
