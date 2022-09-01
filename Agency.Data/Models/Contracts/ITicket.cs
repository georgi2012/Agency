using System;

namespace Agency.Data.Models.Contracts
{
    public interface ITicket
    {
        Guid TicketID { get; }
        decimal AdministrativeCosts { get; }

        Guid JourneyID { get; }
        Journey Journey { get; set; }
        Task<decimal> CalculatePrice();
    }
}