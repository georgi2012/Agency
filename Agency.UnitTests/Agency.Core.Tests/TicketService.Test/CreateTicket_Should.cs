using Agency.Data.Models.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;
using Microsoft.EntityFrameworkCore;
using Agency.Core.Services.TicketServices;
using Agency.Data.Models.Vehicles.Models;

namespace Agency.UnitTests.Agency.Core.Tests.TicketServices.Test
{
    public class CreateTicket_Should
    {

        [Fact]
        public async Task CreateTicket_ShouldAddNewTicketWithCorrectCostAndJourneyObj()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            //mocking
            Mock<Journey> mockJourneyObj = new Mock<Journey>();
            var creationJourney = inmDbContext.Journeys.ToList().First();
            const int admCost = 30;
            int oldListCount = inmDbContext.Tickets.ToList().Count;
            //execution
            var service = new TicketService(inmDbContext);
            await service.CreateTicketAsync(creationJourney, admCost);
            //virification
            var ticketsList = inmDbContext.Tickets.ToList();
            Assert.Equal(oldListCount + 1, ticketsList.Count);
            Ticket ticket = ticketsList.FindLast(x => x.JourneyID == creationJourney.JourneyID);
            Assert.NotNull(ticket);
            Assert.Equal(admCost, ticket.AdministrativeCosts);
        }

       
    }
}
