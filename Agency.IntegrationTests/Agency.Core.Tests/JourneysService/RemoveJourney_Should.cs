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
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tickedService = new(inmDbContext);
            Guid journeyID = new Guid();
            //making sure that the guid is unique
            var journeysList = inmDbContext.Journeys.ToList();
            while (journeysList.Find(x => x.JourneyID == journeyID) != null)
            {
                journeyID = new Guid();
            }
            //act and assert
            var service = new JourneyService(inmDbContext, tickedService);
            Assert.ThrowsAsync<Exception>(async () => await service.RemoveJourneyAsync(journeyID));
        }

        [Fact]
        public async Task RemoveJourney_ShouldRemoveJourneyWhenFoundAndKeepOtherJourneysUntouched()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            var journeyListBefore = inmDbContext.Journeys.ToList();
            TicketService tickedService = new(inmDbContext);
            Assert.True(journeyListBefore.Count >= 2);
            var journeyIdToRemove = journeyListBefore.First().JourneyID;
            var service = new JourneyService(inmDbContext,tickedService);
            //act
            await service.RemoveJourneyAsync(journeyIdToRemove);
            //assert
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
