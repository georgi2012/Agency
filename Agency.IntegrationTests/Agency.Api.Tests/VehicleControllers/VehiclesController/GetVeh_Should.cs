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
    public class GetVeh_Should
    {
        [Fact]
        public async void GetVeh_ShouldReturnTheJourneyWhenExists()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService TService = new(Db);
            JourneyService JService = new(Db, TService);
            VehicleService VService = new(Db, JService);
            VehicleNode VehicleNode = new();
            //act
            var id = Db.Vehicles.ToList().First().VehicleID;
            VehicleController controller = new(VService ,VehicleNode);
            var result = (await controller.GetVeh(id)).Value;
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetVeh_ShouldReturnNotFoundWhenDoesNotExist()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService TService = new(Db);
            JourneyService JService = new(Db, TService);
            VehicleService VService = new(Db, JService);
            VehicleNode VehicleNode = new();
            //act
            VehicleController controller = new(VService,VehicleNode);
            var result = (await controller.GetVeh(42)).Result;
            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }


    }
}
