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
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agency.UnitTests.Agency.Api.Tests.JourneyControllers
{
    public class GetJourney_Should
    {
        [Fact]
        public async void GetJourney_ShouldReturnTheJourneyWhenExists()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            VehicleService vService = new(db, jService);
            JourneyNode journeyNode = new();
            //act
            JourneyController controller = new(jService,
                vService, db, journeyNode);
            var guid = db.Journeys.ToList().First().JourneyID;
            var result = (await controller.GetJourney(guid)).Value;
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetJourney_ShouldReturnNotFoundWhenDoesNotExist()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            VehicleService vService = new(db, jService);
            JourneyNode journeyNode = new();
            //act
            JourneyController controller = new(jService,
                vService, db, journeyNode);
            var result = (await controller.GetJourney(new Guid())).Result;
            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }

    }
}
