using Agency.Api.Controllers.Vechiles;
using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace Agency.UnitTests.Agency.Api.Tests.VehicleControllers.BoatControllers
{
    public class EditBoat_Should
    {
        [Fact]
        public async void EditAirplane_ShouldReturnNotFoundWhenNotFoundSuchVehicleInDb()
        {
            //arrange
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<BoatService> mockBoatService = new(mockDb.Object);
            Mock<BoatReceiveNode> mockNode = new Mock<BoatReceiveNode>();
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<IVehicle>(null));
            mockNode.Object.ID = 42;
            //act
            BoatController controller = new(mockBoatService.Object,
                    mockVService.Object);
            var result = await controller.EditBoat(mockNode.Object);
            //
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }

        [Fact]
        public async void EditAirplane_ShouldReturnOKWhenIsChangedSuccessfully()
        {
            //arrange
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<BoatService> mockBoatService = new(mockDb.Object);
            Mock<BoatReceiveNode> mockNode = new Mock<BoatReceiveNode>();
            Mock<Boat> mockBoat = new();
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<IVehicle>(mockBoat.Object));
            //act
            mockNode.Object.ID = 42;
            //assert
            BoatController controller = new(mockBoatService.Object,
                    mockVService.Object);
            var result = await controller.EditBoat(mockNode.Object);
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public async void EditAirplane_ShouldReturnBadRequestWhenIdIsNotAirplane()
        {
            //arrange
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<BoatService> mockBoatService = new(mockDb.Object);
            Mock<BoatReceiveNode> mockNode = new Mock<BoatReceiveNode>();
            Mock<IBus> mockNOTBoat = new();
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<IVehicle>(mockNOTBoat.Object));
            //act
            BoatController controller = new(mockBoatService.Object,
                    mockVService.Object);
            var result = await controller.EditBoat(mockNode.Object);
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
        }
    }
}
