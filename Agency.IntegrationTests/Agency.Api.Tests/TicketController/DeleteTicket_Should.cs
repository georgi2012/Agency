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
    public class DeleteTicket_Should
    {
        [Fact]
        public async void DeleteJourney_ShouldReturnNotFoundWhenRemoveFailsBecauseNotFound()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            VehicleService vService = new(db, jService);
            JourneyReceiveNode journeyDTO = new();
            TicketNode tNode = new();
            //act
            TicketController controller = new(jService,
                tService, db, tNode);
            var result = await controller.DeleteTicket(new Guid());
            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);

        }

        [Fact]
        public async void DeleteJourney_ShouldReturnOKWhenIsDeletedSuccessfully()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            TicketNode tNode = new();
            //act
            TicketController controller = new(jService,
               tService, db, tNode);
            var id = db.Tickets.ToList().First().TicketID;
            var result = await controller.DeleteTicket(id);
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
