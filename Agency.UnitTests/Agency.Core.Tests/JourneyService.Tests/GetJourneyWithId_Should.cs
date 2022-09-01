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
using Agency.Core.Services.VehicleServices;

namespace Agency.UnitTests.Agency.Core.Tests.JourneysService.Tests
{
    public class GetJourneyWithId_Should
    {
        [Fact]
        public async Task GetJourneyWithId_ShouldReturnNullWhenNotFound()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            //execution
            var service = new JourneyService(inmDbContext,mockTicketService.Object);
            var returnedJourney = await service.GetJourneyWithIdAsync(new Guid());
            //virification
            Assert.Null(returnedJourney);
        }

        [Fact]
        public async Task GetJourneyWithId_ShouldReturnTheCorrectJourney()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            Mock<JourneyService> mockJourneyService = new(inmDbContext, mockTicketService.Object);
            var wantedJourney = inmDbContext.Journeys.ToList().First();
            //execution
            var service = new JourneyService(inmDbContext, mockTicketService.Object);
            var returnedJourney = await service.GetJourneyWithIdAsync(wantedJourney.JourneyID);
            //virification
            Assert.NotNull(returnedJourney);
            Assert.Equal(wantedJourney, returnedJourney);
        }

    }
}
