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
            Mock<ITicket> mockTicket = new();
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<TicketNode> mockTicketNode = new();
            mockTService.Setup(x => x.GetTicketWithIdAsync(It.IsAny<Guid>())).
                Returns(Task.FromResult(mockTicket.Object));
            mockTicketNode.Setup(x => x.MakeTicketNode(
                It.IsAny<ITicket>(), mockDb.Object)).Returns(Task.FromResult(mockTicketNode.Object));
            //act
            TicketController controller = new(mockJService.Object,
                mockTService.Object, mockDb.Object, mockTicketNode.Object);
            var result = (await controller.GetTicket(new Guid())).Value;
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetTicket_ShouldReturnNotFoundWhenDoesNotExist()
        {
            //arrange
            Mock<IJourney> mockJourney = new();
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<TicketNode> mockTicketNode = new();
            mockTService.Setup(x => x.GetTicketWithIdAsync(It.IsAny<Guid>())).
                Returns(Task.FromResult<ITicket>(null));
            //act
            TicketController controller = new(mockJService.Object,
                mockTService.Object, mockDb.Object, mockTicketNode.Object);
            var result = (await controller.GetTicket(new Guid())).Result;
            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }

    }
}
