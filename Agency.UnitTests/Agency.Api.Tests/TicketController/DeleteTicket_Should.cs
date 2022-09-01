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
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            mockTService.Setup(x => x.RemoveTicketAsync(It.IsAny<Guid>())).Throws(new Exception());
            Mock<JourneyReceiveNode> mockJourneyDTO = new();
            Mock<ITicketNode> mockTNode = new();

            //
            TicketController controller = new(mockJService.Object,
                mockTService.Object, mockDb.Object, mockTNode.Object);
            var result = await controller.DeleteTicket(new Guid());
            //
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);

        }

        [Fact]
        public async void DeleteJourney_ShouldReturnOKWhenIsDeletedSuccessfully()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<ITicketNode> mockTNode = new();
            //
            TicketController controller = new(mockJService.Object,
               mockTService.Object, mockDb.Object, mockTNode.Object);
            var result = await controller.DeleteTicket(new Guid());
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
