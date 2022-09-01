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
    public class GetTickets_Should
    {

        [Fact]
        public async Task GetTickets_ShouldReturnAllTicketsIfAnyAreAvailable()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            List<ITicket> returnedTicketsList = null;
            List<Ticket> expectedTicketsList = inmDbContext.Tickets.ToList();
            //act
            var service = new TicketService(inmDbContext);
            returnedTicketsList = await service.GetTicketsAsync();
            //assert
            Assert.NotNull(returnedTicketsList);
            Assert.Equal(expectedTicketsList.Count, returnedTicketsList.Count);

            for(int i= 0; i < returnedTicketsList.Count; i++)
            {
                Assert.Equal(expectedTicketsList[i], returnedTicketsList[i]);
            }
        }

        [Fact]
        public async Task GetTickets_ShouldReturnEmptyListIfDbIsEmpty()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            var service = new TicketService(inmDbContext);
            //act
            List<ITicket> returnedTicketsList = await service.GetTicketsAsync();
            //assert
            Assert.NotNull(returnedTicketsList);
            Assert.True(returnedTicketsList.Count == 0);
        }

    }
}
