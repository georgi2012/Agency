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
    public class DeleteVeh_Should
    {
        [Fact]
        public async void DeleteVeh_ShouldReturnNotFoundWhenRemoveFailsBecauseNotFound()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            mockVService.Setup(x => x.RemoveVehicleAsync(It.IsAny<int>())).Throws(new Exception());
            Mock<JourneyReceiveNode> mockJourneyDTO = new();
            Mock<IVehicleNode> mockVNode = new();

            //
            VehicleController controller = new(mockVService.Object, mockVNode.Object);
            var result = await controller.DeleteVeh(new Random().Next(10,1000));
            //
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);

        }

        [Fact]
        public async void DeleteVeh_ShouldReturnOKWhenIsDeletedSuccessfully()
        {
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<IVehicleNode> mockVNode = new();
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            //
            VehicleController controller = new(mockVService.Object,mockVNode.Object);
            var result = await controller.DeleteVeh(new Random().Next(10, 1000));
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

    }
}
