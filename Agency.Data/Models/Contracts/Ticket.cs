using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Agency.Data.DB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.Data.Models.Contracts
{
    public class Ticket : ITicket
    {
        [Key]

        public virtual Guid TicketID { get; set; }
        public virtual decimal AdministrativeCosts { get;  set; }
        
        //Navigation prroperties
        public virtual Guid JourneyID { get;  set; }

        public virtual Journey Journey { get; set; }
        

        public Ticket()
        {}

        public Ticket(decimal administrativeCosts , Journey journey)
        {
            AdministrativeCosts = administrativeCosts;
            JourneyID = journey.JourneyID;
            Journey = journey;
        }

        public async Task<decimal> CalculatePrice()
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
