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
    public class GetTicketWithId_Should
    {
        [Fact]
        public async Task GetTicketWithId_ShouldReturnNullWhenNotFound()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            var service = new TicketService(inmDbContext);
            //act
            var returnedTicket = await service.GetTicketWithIdAsync(new Guid());
            //assert
            Assert.Null(returnedTicket);
        }

        [Fact]
        public async Task GetTicketWithId_ShouldReturnTheCorrectTicket()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            var wantedTicket = inmDbContext.Tickets.ToList().First();
            //act
            var service = new TicketService(inmDbContext);
            var returnedTicket = await service.GetTicketWithIdAsync(wantedTicket.TicketID);
            //assert
            Assert.NotNull(returnedTicket);
            Assert.Equal(wantedTicket, returnedTicket);
        }
    }
}
