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
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            AirplaneService planeService = new(Db);
            AirplaneReceiveNode node = new AirplaneReceiveNode();
            node.ID = 42;
            //act
            AirplaneController controller = new(planeService,
                    vService);
            var result = await controller.EditAirplane(node);
            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }

        [Fact]
        public async void EditAirplane_ShouldReturnOKWhenIsChangedSuccessfully()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            AirplaneService planeService = new(Db);
            AirplaneReceiveNode node = new AirplaneReceiveNode();
            Airplane Airplane = new();
            //
            node.ID = Db.Vehicles.ToList().Find(x=>x is Airplane).VehicleID;
            node.PricePerKilometer = 3m;
            node.HasFreeFood = true;
            node.PassangerCapacity = 600;
            //act
            AirplaneController controller = new(planeService,
                    vService);
            var result = await controller.EditAirplane(node);
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public async void EditAirplane_ShouldReturnBadRequestWhenIdIsNotAirplane()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            AirplaneService planeService = new(Db);
            AirplaneReceiveNode node = new AirplaneReceiveNode();
            var NOTAirplane = Db.Vehicles.ToList().Find(x=>x is Bus);
            node.ID = NOTAirplane.VehicleID;
            //act
            AirplaneController controller = new(planeService,
                    vService);
            var result = await controller.EditAirplane(node);
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
        }
    }
}
