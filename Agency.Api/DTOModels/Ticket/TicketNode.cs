using Agency.Data.DB;
using Agency.Data.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Agency.Api.DTOModels.Ticket
{
    public class TicketNode : ITicketNode
    {

        public TicketNode()
        {

        }
        public TicketNode(string id, decimal cost, string jId, decimal totalPrice, string from, string to)
        {
            TicketID = id;
            AdministrativeCosts = cost;
            TotalPrice = totalPrice;
            JourneyID = jId;
            From = from;
            To = to;
        }

        public string TicketID { get; set; }
        public decimal AdministrativeCosts { get; set; }
        public string JourneyID { get; }
        public decimal TotalPrice { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public virtual async Task<TicketNode> MakeTicketNode(ITicket ticket, AgencyDBContext dBContext)
        {
            var journey = dBContext.Journeys.Include(e => e.Vehicle).FirstOrDefault(el => el.JourneyID == ticket.JourneyID);
            if (journey == null)
            {
                throw new Exception("Ticket's journey could not be found in the database");
            }

            return new TicketNode(ticket.TicketID.ToString(), ticket.AdministrativeCosts,
             journey.JourneyID.ToString(), await ticket.CalculatePrice(),
                    journey.StartLocation, journey.Destination);
        }
        public virtual async Task<List<TicketNode>> MakeListOfNodes(List<ITicket> tickets, AgencyDBContext dBContext)
        {
            List<TicketNode> ticketNodes = new List<TicketNode>();
            foreach (var ticket in tickets)
            {
                ticketNodes.Add(await MakeTicketNode(ticket, dBContext));
            }
            return ticketNodes;
        }
    }
}
