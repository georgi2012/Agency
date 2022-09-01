using System.ComponentModel.DataAnnotations;

namespace Agency.Api.DTOModels.Ticket
{
    public class TicketReceiveNode
    {
        [Range(0, int.MaxValue, ErrorMessage = "Invalid administrative cost. Should not be negative.")]
        public decimal AdministrativeCosts { get; set; }
        public string JourneyID { get; set; }
    }
}
