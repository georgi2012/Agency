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
using Agency.Core.Services.JourneyServices;

namespace Agency.UnitTests.Agency.Core.Tests.JourneysService.Tests
{
    public class GetJourneys_Should
    {
        [Fact]
        public async Task GetJourneys_ShouldReturnAllJourneysIfAnyAreAvailable()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            List<IJourney> returnedTicketsList = null;
            List<Journey> expectedTicketsList = inmDbContext.Journeys.ToList();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            //execution
            var service = new JourneyService(inmDbContext, mockTicketService.Object);
            returnedTicketsList = await service.GetJourneyAsync();
            //virification
            Assert.NotNull(returnedTicketsList);
            Assert.Equal(expectedTicketsList.Count, returnedTicketsList.Count);

            for (int i = 0; i < returnedTicketsList.Count; i++)
            {
                Assert.Equal(expectedTicketsList[i], returnedTicketsList[i]);
            }
        }

        [Fact]
        public async Task GetJourneys_ShouldReturnEmptyListIfDbIsEmpty()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            //execution
            var service = new JourneyService(inmDbContext,mockTicketService.Object);
            var returnedTicketsList = await service.GetJourneyAsync();
            //virification
            Assert.NotNull(returnedTicketsList);
            Assert.True(returnedTicketsList.Count == 0);
        }
    }
}
