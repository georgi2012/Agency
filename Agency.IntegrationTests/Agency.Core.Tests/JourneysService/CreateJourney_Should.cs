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
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            var creationVeh = inmDbContext.Vehicles.ToList().First();
            TicketService ticketService = new(inmDbContext);
            const int distance = 350;
            string location = "abudabi";
            string destination = "dolno poduene";
            int oldListCount = inmDbContext.Tickets.ToList().Count;
            var service = new JourneyService(inmDbContext, ticketService);
            //act
            await service.CreateJourneyAsync(location,destination,distance, creationVeh);
            var journeysList = inmDbContext.Journeys.ToList();
            //assert
            Assert.Equal(oldListCount + 1, journeysList.Count);
            var journey = journeysList.FindLast(x => x.VehicleID == creationVeh.VehicleID);
            Assert.NotNull(journey);
            Assert.Equal(distance, journey.Distance);
            Assert.Equal(location, journey.StartLocation);
            Assert.Equal(destination, journey.Destination);
        }

    }
}
