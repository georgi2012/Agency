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

namespace Agency.UnitTests.Agency.Api.Tests.VehicleControllers.AirplaneControllers
{
    public class EditAirplane_Should
    {
        [Fact]
        public async void EditAirplane_ShouldReturnNotFoundWhenNotFoundSuchVehicleInDb()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<AirplaneService> mockPlaneService = new(mockDb.Object);
            Mock<AirplaneReceiveNode> mockNode = new Mock<AirplaneReceiveNode>();
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<IVehicle>(null));
            mockNode.Object.ID = 42;
            //
            AirplaneController controller = new(mockPlaneService.Object,
                    mockVService.Object);
            var result = await controller.EditAirplane(mockNode.Object);
            //
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }

        [Fact]
        public async void EditAirplane_ShouldReturnOKWhenIsChangedSuccessfully()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<AirplaneService> mockPlaneService = new(mockDb.Object);
            Mock<AirplaneReceiveNode> mockNode = new Mock<AirplaneReceiveNode>();
            Mock<Airplane> mockAirplane = new();
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<IVehicle>(mockAirplane.Object));
            //
            mockNode.Object.ID = 42;
            mockNode.Object.PricePerKilometer = 3m;
            mockNode.Object.HasFreeFood = true;
            mockNode.Object.PassangerCapacity = 600;
            //
            AirplaneController controller = new(mockPlaneService.Object,
                    mockVService.Object);
            var result = await controller.EditAirplane(mockNode.Object);
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public async void EditAirplane_ShouldReturnBadRequestWhenIdIsNotAirplane()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<AirplaneService> mockPlaneService = new(mockDb.Object);
            Mock<AirplaneReceiveNode> mockNode = new Mock<AirplaneReceiveNode>();
            Mock<IBus> mockNOTAirplane = new();
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<IVehicle>(mockNOTAirplane.Object));
            //
            AirplaneController controller = new(mockPlaneService.Object,
                    mockVService.Object);
            var result = await controller.EditAirplane(mockNode.Object);
            //
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
        }
    }
}
