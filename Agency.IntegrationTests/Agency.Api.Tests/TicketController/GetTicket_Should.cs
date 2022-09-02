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
    public class GetTicket_Should
    {
        [Fact]
        public async void GetTicket_ShouldReturnTheJourneyWhenExists()
        {
            //arrange
            Ticket Ticket = new();
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService TService = new(Db);
            JourneyService JService = new(Db, TService);
            VehicleService VService = new(Db, JService);
            TicketNode TicketNode = new();
            //act
            TicketController controller = new(JService,
                TService, Db, TicketNode);
            var id = Db.Tickets.ToList().First().TicketID;
            var result = (await controller.GetTicket(id)).Value;
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetTicket_ShouldReturnNotFoundWhenDoesNotExist()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService TService = new(Db);
            JourneyService JService = new(Db, TService);
            VehicleService VService = new(Db, JService);
            TicketNode TicketNode = new();
            //act
            TicketController controller = new(JService,
                TService, Db, TicketNode);
            var result = (await controller.GetTicket(new Guid())).Result;
            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }

    }
}
