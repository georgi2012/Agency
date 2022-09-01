using Agency.Data.DB;
using Agency.Core.Contracts;
using Agency.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Agency.Core.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly AgencyDBContext _dbContext;

        public TicketService(AgencyDBContext db)
        {
            this._dbContext = db;
        }

        public virtual async Task<List<ITicket>> GetTicketsAsync() =>
            await _dbContext.Tickets.ToListAsync<ITicket>();

        public virtual async Task<ITicket> GetTicketWithIdAsync(Guid id)
            => await _dbContext.Tickets.FirstOrDefaultAsync(el => el.TicketID == id);

        public virtual async Task CreateTicketAsync(IJourney journey, decimal admCost)
        {
            await _dbContext.Tickets.AddAsync(new Ticket(admCost, (Journey)journey));
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveTicketAsync(Guid id)
        {
            var ticket = (Ticket)(await GetTicketWithIdAsync(id));
            if(ticket == null)
            {
                throw new Exception("Ticket with such ID could not be found");
            }
            _dbContext.Tickets.Remove(ticket);
            await _dbContext.SaveChangesAsync();
        }


    }
}
