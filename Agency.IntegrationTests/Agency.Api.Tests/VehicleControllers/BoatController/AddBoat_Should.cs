using Agency.Api.Controllers.Vechiles;
using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Agency.UnitTests.Agency.Api.Tests.VehicleControllers.BoatControllers
{
    public class AddBoat_Should
    {
        [Fact]
        public async void AddBoat_ShouldReturnBadRequestWhenCreationFailed()
        {
            //arrange
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<BoatService> mockBoatService = new(mockDb.Object);
            Mock<BoatReceiveNode> mockNode = new Mock<BoatReceiveNode>();
            mockBoatService.Setup(x => x.CreateBoatAsync(It.IsAny<int>(),
                It.IsAny<decimal>(), It.IsAny<bool>())).Throws(new Exception());
            //act
            BoatController controller = new(mockBoatService.Object,
                    mockVService.Object);
            var result = await controller.AddBoat(mockNode.Object);
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);

        }

        [Fact]
        public async void AddBoat_ShouldReturnOKWhenIsCreatedSuccessfully()
        {
            //arrange
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<BoatService> mockPlaneService = new(mockDb.Object);
            Mock<BoatReceiveNode> mockNode = new Mock<BoatReceiveNode>();
            //act
            BoatController controller = new(mockPlaneService.Object,
                    mockVService.Object);
            var result = await controller.AddBoat(mockNode.Object);
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
