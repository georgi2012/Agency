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
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agency.UnitTests.Agency.Api.Tests.JourneyControllers
{
    public class GetJourney_Should
    {
        [Fact]
        public async void GetJourney_ShouldReturnTheJourneyWhenExists()
        {
            //arrange
            Mock<IJourney> mockJourney = new();
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<JourneyNode> mockJourneyNode = new();
            mockJService.Setup(x => x.GetJourneyWithIdAsync(It.IsAny<Guid>())).
                Returns(Task.FromResult(mockJourney.Object));
            mockJourneyNode.Setup(x => x.MakeJourneyNode(
                It.IsAny<IJourney>(), mockDb.Object)).Returns(Task.FromResult(mockJourneyNode.Object));
            //act
            JourneyController controller = new(mockJService.Object,
                mockVService.Object, mockDb.Object, mockJourneyNode.Object);
            var result = (await controller.GetJourney(new Guid())).Value;
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetJourney_ShouldReturnNotFoundWhenDoesNotExist()
        {
            //arrange
            Mock<IJourney> mockJourney = new();
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<JourneyNode> mockJourneyNode = new();
            mockJService.Setup(x => x.GetJourneyWithIdAsync(It.IsAny<Guid>())).
                Returns(Task.FromResult<IJourney>(null));
            //act
            JourneyController controller = new(mockJService.Object,
                mockVService.Object, mockDb.Object, mockJourneyNode.Object);
            var result = (await controller.GetJourney(new Guid())).Result;
            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }

    }
}
