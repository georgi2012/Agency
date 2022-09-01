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
    public class AddTicket_Should
    {
        [Fact]
        public async void AddTicket_ShouldReturnBadRequestWhenJourneyIsNotFound()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            mockJService.Setup(x => x.GetJourneyWithIdAsync(It.IsAny<Guid>()))
                .Returns<IJourney>(null);
            Mock<TicketReceiveNode> mockTicketDTO = new();
            Mock<ITicketNode> mockTNode = new();
            //
            TicketController controller = new(mockJService.Object, mockTService.Object,
                 mockDb.Object, mockTNode.Object);
            var result = await controller.AddTicket(mockTicketDTO.Object);
            //
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);

        }

        [Fact]
        public async void AddTicket_ShouldReturnOKWhenIsCreatedSuccessfully()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<IJourney> mockJourney = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            mockJService.Setup(x => x.GetJourneyWithIdAsync(It.IsAny<Guid>())).
                Returns(Task.FromResult(mockJourney.Object));
            Mock<TicketReceiveNode> mockTicketDTO = new();
            mockTicketDTO.Object.JourneyID = new Guid().ToString();
            Mock<ITicketNode> mockTNode = new();
            //
            TicketController controller = new(mockJService.Object, mockTService.Object,
                 mockDb.Object, mockTNode.Object);
            var result = await controller.AddTicket(mockTicketDTO.Object);
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

    }
}
