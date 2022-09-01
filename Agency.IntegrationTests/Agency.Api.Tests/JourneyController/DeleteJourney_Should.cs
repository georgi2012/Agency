using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Api.Controllers.Journey;
using Agency.Api.DTOModels.Journey;
using Agency.Api.DTOModels.Ticket;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agency.UnitTests.Agency.Api.Tests.JourneyControllers
{
    public class DeleteJourney_Should
    {
        [Fact]
        public async void DeleteTicket_ShouldReturnNotFoundWhenRemoveFailsBecauseNotFound()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            VehicleService vService = new(db, jService);
            JourneyReceiveNode mockJourneyDTO = new();
            JourneyNode mockjNode = new();
            //act
            JourneyController controller = new(jService,
                vService, db, mockjNode);
            var result = await controller.DeleteJourney(new Guid());
            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);

        }

        [Fact]
        public async void DeleteTicket_ShouldReturnOKWhenIsDeletedSuccessfully()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            VehicleService vService = new(db, jService);
            JourneyNode jNode = new();
            //act
            JourneyController controller = new(jService,
                vService, db, jNode);
            var result = await controller.DeleteJourney(
                jService.GetJourneyAsync().Result.First().JourneyID);
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
