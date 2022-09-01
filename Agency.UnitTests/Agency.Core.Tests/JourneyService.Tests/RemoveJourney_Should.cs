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
    public class RemoveJourney_Should
    {

        [Fact]
        public async Task RemoveJourney_ShouldThrowWhenNotFound()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            Mock<TicketService> mockTickedService = new(inmDbContext);
            Guid journeyID = new Guid();
            //making sure that the guid is unique
            var journeysList = inmDbContext.Journeys.ToList();
            while (journeysList.Find(x => x.JourneyID == journeyID) != null)
            {
                journeyID = new Guid();
            }
            //execution and verification
            var service = new JourneyService(inmDbContext, mockTickedService.Object);
            Assert.ThrowsAsync<Exception>(async () => await service.RemoveJourneyAsync(journeyID));
        }

        [Fact]
        public async Task RemoveJourney_ShouldRemoveJourneyWhenFoundAndKeepOtherJourneysUntouched()
        {
            //define
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            var journeyListBefore = inmDbContext.Journeys.ToList();
            Mock<TicketService> mockTickedService = new(inmDbContext);
            Assert.True(journeyListBefore.Count >= 2);
            var journeyIdToRemove = journeyListBefore.First().JourneyID;
            var service = new JourneyService(inmDbContext, mockTickedService.Object);
            mockTickedService.Setup(x => x.GetTicketsAsync()).
                Returns(Task.FromResult(new List<ITicket>()));
            //execute
            await service.RemoveJourneyAsync(journeyIdToRemove);
            //verify
            var journeyListAfter = inmDbContext.Journeys.ToList();
            Assert.NotNull(journeyListAfter);
            Assert.Equal(journeyListBefore.Count - 1, journeyListAfter.Count);
            foreach (var ticket in journeyListAfter)
            {
                Assert.True(journeyListBefore.Find(x => x.JourneyID == ticket.JourneyID) != null);
            }
            Assert.True(journeyListAfter.Find(x => x.JourneyID == journeyIdToRemove) == null);
        }

    }
}
