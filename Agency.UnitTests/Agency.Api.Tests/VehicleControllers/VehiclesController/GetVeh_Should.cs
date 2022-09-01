using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Api.Controllers.Journey;
using Agency.Api.Controllers.Ticket;
using Agency.Api.Controllers.Vechiles;
using Agency.Api.DTOModels.Journey;
using Agency.Api.DTOModels.Ticket;
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

namespace Agency.UnitTests.Agency.Api.Tests.VehicleControllers.VehiclesController
{
    internal class GetVeh_Should
    {
        [Fact]
        public async void GetVeh_ShouldReturnTheJourneyWhenExists()
        {
            Mock<IVehicle> mockVeh = new();
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<VehicleNode> mockVehicleNode = new();
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult(mockVeh.Object));
            mockVehicleNode.Setup(x => x.MakeNodeFromVehicle(
                It.IsAny<IVehicle>())).Returns(Task.FromResult(mockVehicleNode.Object));
            //
            VehicleController controller = new(mockVService.Object ,mockVehicleNode.Object);
            var result = (await controller.GetVeh(42)).Value;
            //
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetVeh_ShouldReturnNotFoundWhenDoesNotExist()
        {
            Mock<IVehicle> mockVeh = new();
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<VehicleNode> mockVehicleNode = new();
            mockVService.Setup(x => x.GetVehicleWithIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<IVehicle>(null));
            //
            VehicleController controller = new(mockVService.Object,mockVehicleNode.Object);
            var result = (await controller.GetVeh(42)).Result;
            //
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }


    }
}
