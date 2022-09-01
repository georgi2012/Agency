using System;

namespace Agency.Data.Models.Contracts
{
    public interface ITicket
    {
        Guid TicketID { get; }
        decimal AdministrativeCosts { get; }

        IJourney Journey { get; }

        decimal CalculatePrice();
    }
}