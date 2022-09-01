using Agency.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Core.Contracts
{
    public interface ITicketService
    {
        Task CreateTicketAsync(IJourney journey, decimal admCost);
        Task RemoveTicketAsync(Guid id);
        Task<List<ITicket>> GetTicketsAsync();
        Task<ITicket> GetTicketWithIdAsync(Guid id);
    }
}
