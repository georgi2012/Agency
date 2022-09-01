using Agency.Data.DB;
using Agency.Data.Models.Contracts;

namespace Agency.Api.DTOModels.Ticket
{
    public interface ITicketNode
    {
        decimal AdministrativeCosts { get; set; }
        string From { get; set; }
        string JourneyID { get; }
        string TicketID { get; set; }
        string To { get; set; }
        decimal TotalPrice { get; set; }

        Task<TicketNode> MakeTicketNode(ITicket ticket, AgencyDBContext dBContext);
        Task<List<TicketNode>> MakeListOfNodes(List<ITicket> tickets, AgencyDBContext dBContext);
    }
}