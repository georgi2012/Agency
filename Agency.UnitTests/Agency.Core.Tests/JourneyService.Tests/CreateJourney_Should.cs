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
    public class CreateJourney_Should
    {
        [Fact]
        public async Task CreateJourney_ShouldAddNewJourneyWithCorrectDataAndVehObj()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            //mocking
            var creationVeh = inmDbContext.Vehicles.ToList().First();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            const int distance = 350;
            string location = "abudabi";
            string destination = "dolno poduene";
            int oldListCount = inmDbContext.Tickets.ToList().Count;
            //execution
            var service = new JourneyService(inmDbContext, mockTicketService.Object);
            await service.CreateJourneyAsync(location,destination,distance, creationVeh);
            //virification
            var journeysList = inmDbContext.Journeys.ToList();
            Assert.Equal(oldListCount + 1, journeysList.Count);
            var journey = journeysList.FindLast(x => x.VehicleID == creationVeh.VehicleID);
            Assert.NotNull(journey);
            Assert.Equal(distance, journey.Distance);
            Assert.Equal(location, journey.StartLocation);
            Assert.Equal(destination, journey.Destination);
        }

    }
}
