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
        public async void EditBoat_ShouldReturnNotFoundWhenNotFoundSuchVehicleInDb()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            BoatService boatService = new(Db);
            BoatReceiveNode node = new BoatReceiveNode();
            node.ID = 42;
            //act
            BoatController controller = new(boatService,
                    vService);
            var result = await controller.EditBoat(node);
            //
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }

        [Fact]
        public async void EditBoat_ShouldReturnOKWhenIsChangedSuccessfully()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            BoatService boatService = new(Db);
            BoatReceiveNode node = new BoatReceiveNode();
            //act
            node.ID = Db.Vehicles.ToList().Find(x=>x is Boat).VehicleID;
            node.PricePerKilometer = 3m;
            node.HasWaterSports = true;
            node.PassangerCapacity = 600;
            //assert
            BoatController controller = new(boatService,
                    vService);
            var result = await controller.EditBoat(node);
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public async void EditBoat_ShouldReturnBadRequestWhenIdIsNotAirplane()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            BoatService boatService = new(Db);
            BoatReceiveNode node = new BoatReceiveNode();
            node.ID = Db.Vehicles.ToList().Find(x => x is Bus).VehicleID;
            //act
            BoatController controller = new(boatService, vService);
            var result = await controller.EditBoat(node);
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
        }
    }
}
