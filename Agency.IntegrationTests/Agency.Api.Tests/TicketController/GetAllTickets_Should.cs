using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Api.Controllers.Journey;
using Agency.Api.Controllers.Ticket;
using Agency.Api.DTOModels.Journey;
using Agency.Api.DTOModels.Ticket;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agency.UnitTests.Agency.Api.Tests.TicketControllers
{
    public class GetAllTickets_Should
    {
        [Fact]
        public async void GetAllTickets_ShouldReturnListOfAllAvailableJourneys()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService TService = new(db);
            JourneyService JService = new(db, TService);
            VehicleService VService = new(db, JService);
            TicketNode TicketNode = new();
            var ticketsList= db.Tickets.ToList();
            //act
            TicketController controller = new(JService,
                TService, db, TicketNode);
            var result = (await controller.GetAllTickets()).Value;
            //assert
            Assert.NotNull(result);
            Assert.Equal(ticketsList.Count, result.Count);
            foreach (var item in ticketsList)
            {
                Assert.True(result.Find(x => x.JourneyID == item.JourneyID.ToString()) != null);
            }
        }

    }
}
