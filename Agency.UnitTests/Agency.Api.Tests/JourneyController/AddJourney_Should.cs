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
    public class AddJourney_Should
    {

        [Fact]
        public async void AddJourney_ShouldReturnBadRequestWhenVehicleIsNotFound()
        {
            //arrange
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object,mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).Returns(Task.FromResult<IVehicle>(null));
            Mock<JourneyReceiveNode> mockJourneyDTO = new();
            Mock<IJourneyNode> mockJNode = new();
            //act
            JourneyController controller = new(mockJService.Object,
                mockVService.Object, mockDb.Object, mockJNode.Object);
            var result =await controller.AddJourney(mockJourneyDTO.Object);
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);

        }

        [Fact]
        public async void AddJourney_ShouldReturnOKWhenIsCreatedSuccessfully()
        {
            //arrange
            Mock<AgencyDBContext> mockDb = new();
            Mock<IVehicle> mockVeh = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult(mockVeh.Object));
            Mock<JourneyReceiveNode> mockJourneyDTO = new();
            Mock<IJourneyNode> mockJNode = new();

            //act
            JourneyController controller = new(mockJService.Object,
                mockVService.Object, mockDb.Object,mockJNode.Object);
            var result = await controller.AddJourney(mockJourneyDTO.Object);
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
