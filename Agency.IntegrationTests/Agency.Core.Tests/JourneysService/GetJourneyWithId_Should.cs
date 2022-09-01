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
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            TicketService ticketService = new(inmDbContext);
            //act
            var service = new JourneyService(inmDbContext,ticketService);
            var returnedJourney = await service.GetJourneyWithIdAsync(new Guid());
            //assert
            Assert.Null(returnedJourney);
        }

        [Fact]
        public async Task GetJourneyWithId_ShouldReturnTheCorrectJourney()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            TicketService ticketService = new(inmDbContext);
            var wantedJourney = inmDbContext.Journeys.ToList().First();
            var service = new JourneyService(inmDbContext, ticketService);
            //act
            var returnedJourney = await service.GetJourneyWithIdAsync(wantedJourney.JourneyID);
            //assert
            Assert.NotNull(returnedJourney);
            Assert.Equal(wantedJourney, returnedJourney);
        }

    }
}
