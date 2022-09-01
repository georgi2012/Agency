using Agency.Api.Controllers.Vechiles;
using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agency.UnitTests.Agency.Api.Tests.VehicleControllers.AirplaneControllers
{
    public class AddAirplane_Should
    {
        [Fact]
        public async void AddAirplane_ShouldReturnBadRequestWhenCreationFailed()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<AirplaneService> mockPlaneService = new(mockDb.Object);
            Mock<AirplaneReceiveNode> mockNode = new Mock<AirplaneReceiveNode>();
            mockPlaneService.Setup(x => x.CreateAirplaneAsync(It.IsAny<int>(),
                It.IsAny<decimal>(), It.IsAny<bool>())).Throws(new Exception());
            //
            AirplaneController controller = new(mockPlaneService.Object,
                    mockVService.Object);
            var result = await controller.AddAirplane(mockNode.Object);
            //
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);

        }

        [Fact]
        public async void AddAirplane_ShouldReturnOKWhenIsCreatedSuccessfully()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<AirplaneService> mockPlaneService = new(mockDb.Object);
            Mock<AirplaneReceiveNode> mockNode = new Mock<AirplaneReceiveNode>();
            //
            AirplaneController controller = new(mockPlaneService.Object,
                    mockVService.Object);
            var result = await controller.AddAirplane(mockNode.Object);
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
