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
    public class GetAllVehs_Should
    {
        [Fact]
        public async void GetAllVehs_ShouldReturnListOfAllAvailableJourneys()
        {
            Mock<VehicleNode> mockJ1 = new();
            Mock<VehicleNode> mockJ2 = new();
            Mock<VehicleNode> mockJ3 = new();
            List<VehicleNode> vehsList = new() { mockJ1.Object, mockJ2.Object, mockJ3.Object };
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<IVehicleNode> mockVehicleNode = new();
            mockVehicleNode.Setup(x => x.MakeNodeListFromVehiclesList(
                It.IsAny<List<IVehicle>>())).Returns(Task.FromResult(vehsList));
            //
            VehicleController controller = new(mockVService.Object,mockVehicleNode.Object);
            var result = (await controller.GetAllVehs()).Value;
            //
            Assert.NotNull(result);
            Assert.Equal(vehsList.Count, result.Count);
        }

    }
}
