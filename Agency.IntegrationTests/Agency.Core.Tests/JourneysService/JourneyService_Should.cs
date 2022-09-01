using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;
using Microsoft.EntityFrameworkCore;
using Agency.Core.Services.TicketServices;
using Agency.Data.Models.Vehicles.Models;
using Agency.Core.Services.JourneyServices;
using Agency.UnitTests;
using Agency.Data.Models.Contracts;

namespace Agency.IntegrationTests.JourneysService
{
    public class JourneyService_Should
    {
        [Fact]
        public async void JourneyService_ShouldRemoveTicketsThatUseTheJourneyAfterItIsRemoved()
        {
            const int cost1 = 100;
            const int cost2 = 200;
            const int distance = 350;
            string location = "abudabi";
            string destination = "dolno poduene";
            //
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            var creationVeh = inmDbContext.Vehicles.ToList().First();
            TicketService ticketService = new TicketService(inmDbContext);
            JourneyService journeyService = new JourneyService(inmDbContext, ticketService);
            //make some journey
            await journeyService.CreateJourneyAsync(location, destination, distance, creationVeh);
            //find the added ticket for the ID
            IJourney journey = (await journeyService.GetJourneyAsync()).
                Find(x=>x.VehicleID==creationVeh.VehicleID && x.Destination == destination);
            var ticketsBeforeAdding = await ticketService.GetTicketsAsync();
            //make tickets with it
            await ticketService.CreateTicketAsync(journey,cost1);
            await ticketService.CreateTicketAsync(journey,cost2);
            //making sure that both tickets are added
            Assert.Equal(ticketsBeforeAdding.Count+2,(await ticketService.GetTicketsAsync()).Count);
            //now remove the journey
            await journeyService.RemoveJourneyAsync(journey.JourneyID);
            //and check if tickets are also removed
            var ticketsAfterRemoving = await ticketService.GetTicketsAsync();
            Assert.Equal(ticketsBeforeAdding.Count, ticketsAfterRemoving.Count);
            var ticketsThatUsedTheJourney = ticketsAfterRemoving.FindAll(x => x.JourneyID == journey.JourneyID);
            Assert.True(ticketsThatUsedTheJourney.Count == 0);

        }

    }
}
