using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Api.Controllers.Journey;
using Agency.Api.DTOModels.Journey;
using Agency.Api.DTOModels.Ticket;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agency.UnitTests.Agency.Api.Tests.JourneyControllers
{
    public class AddJourney_Should
    {

        [Fact]
        public async void AddJourney_ShouldReturnBadRequestWhenVehicleIsNotFound()
        {
            //arrange
            AgencyDBContext Db = new();
            TicketService tService = new(Db);
            JourneyService jService = new(Db,tService);
            VehicleService vService = new(Db, jService);
            JourneyReceiveNode journeyDTO = new();
            JourneyNode JNode = new();
            //act
            JourneyController controller = new(jService,vService, Db, JNode);
            var result =await controller.AddJourney(journeyDTO);
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);

        }

        [Fact]
        public async void AddJourney_ShouldReturnOKWhenIsCreatedSuccessfully()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            JourneyReceiveNode journeyDTO = new()
            {
                Destination = "alabama",
                Distance = 100,
                VehicleID = (await vService.GetVehiclesAsync()).
                First().VehicleID,
                StartLocation="home"

            };
            JourneyNode JNode = new();
            //act
            JourneyController controller = new(jService,
                vService, Db,JNode);
            var result = await controller.AddJourney(journeyDTO);
            //
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
