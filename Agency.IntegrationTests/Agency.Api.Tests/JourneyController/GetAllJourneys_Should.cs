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
    public class GetAllJourneys_Should
    {
        [Fact]
        public async void GetAllJourneys_ShouldReturnListOfAllAvailableJourneys()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemorySeededContextGenerator();
            var journeysList = db.Journeys.ToList();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            VehicleService vService = new(db, jService);
            JourneyNode journeyNode = new();
            //act
            JourneyController controller = new(jService,
                vService, db, journeyNode);
            var result =(await controller.GetAllJourneys()).Value;
            //assert
            Assert.NotNull(result);
            Assert.Equal(journeysList.Count,result.Count);
            foreach (var item in journeysList)
            {
                Assert.True(result.Find(x=>x.JourneyID == item.JourneyID.ToString()) != null);
            }
        }

    }
}
