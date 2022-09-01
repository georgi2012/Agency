using System;
using Agency.Data.Models.Contracts;
using Moq;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;
using Microsoft.EntityFrameworkCore;
using Agency.Core.Services.TicketServices;
using Agency.Data.Models.Vehicles.Models;
namespace Agency.UnitTests.Agency.Core.Tests.TicketServices.Test
{
    public class RemoveTicket_Should
    {

        [Fact]
        public async Task RemoveTicket_ShouldThrowWhenNotFound()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            Guid tickedID = new Guid();
            //making sure that the guid is unique
            var ticketsList = inmDbContext.Tickets.ToList();
            while (ticketsList.Find(x => x.TicketID == tickedID) != null)
            {
                tickedID = new Guid();
            }
            var service = new TicketService(inmDbContext);
            //act and assert
            Assert.ThrowsAsync<Exception>(async () => await service.RemoveTicketAsync(tickedID));
        }

        [Fact]
        public async Task RemoveTicket_ShouldRemoveTicketWhenFoundAndKeepOtherTicketsUntouched()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            var ticketsListBefore = inmDbContext.Tickets.ToList();
            Assert.True(ticketsListBefore.Count >= 2);
            var ticketIdToRemove = ticketsListBefore.First().TicketID;
            var service = new TicketService(inmDbContext);
            //act
            await service.RemoveTicketAsync(ticketIdToRemove);
            //assert
            var ticketsListAfter = inmDbContext.Tickets.ToList();
            Assert.NotNull(ticketsListAfter);
            Assert.Equal(ticketsListBefore.Count - 1, ticketsListAfter.Count);
            foreach (var ticket in ticketsListAfter)
            {
                Assert.True(ticketsListBefore.Find(x => x.TicketID == ticket.TicketID) != null);
            }
            Assert.True(ticketsListAfter.Find(x => x.TicketID == ticketIdToRemove) == null);
        }

    }
}
