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
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            mockJService.Setup(x => x.RemoveJourneyAsync(It.IsAny<Guid>())).Throws(new Exception());
            Mock<JourneyReceiveNode> mockJourneyDTO = new();
            Mock<IJourneyNode> mockJNode = new();

            //
            JourneyController controller = new(mockJService.Object,
                mockVService.Object, mockDb.Object, mockJNode.Object);
            var result = await controller.DeleteJourney(new Guid());
            //
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);

        }

        [Fact]
        public async void DeleteTicket_ShouldReturnOKWhenIsDeletedSuccessfully()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<IJourneyNode> mockJNode = new();
            //
            JourneyController controller = new(mockJService.Object,
                mockVService.Object, mockDb.Object, mockJNode.Object);
            var result = await controller.DeleteJourney(new Guid());
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
