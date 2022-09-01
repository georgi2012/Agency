using System;
using System.Collections.Generic;
using System.Text;

namespace Agency.Data.Models.Contracts
{
    public class Ticket : ITicket
    {
        private readonly Guid ID;
        public Guid TicketID { get => ID; }
        public decimal AdministrativeCosts { get; private set; }
        public IJourney Journey { get; private set; }

        public Ticket(decimal administrativeCosts, IJourney journey)
        {
            AdministrativeCosts = administrativeCosts;
            Journey = journey;
            ID = Guid.NewGuid();
        }


        public decimal CalculatePrice()
        {
            return AdministrativeCosts + Journey.CalculateTravelCosts();
        }

        public override string ToString()
        {
            return "Ticket ----\n" +
                   $"Destination: {Journey.Destination}\n" +
                   $"Price: {CalculatePrice()}";
        }
    }
}
