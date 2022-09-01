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
            Mock<JourneyNode> mockJ1 = new();
            Mock<JourneyNode> mockJ2 = new();
            Mock<JourneyNode> mockJ3 = new();
            List<JourneyNode> journeysList = new() { mockJ1.Object, mockJ2.Object, mockJ3.Object };
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<IJourneyNode> mockJourneyNode = new();
            mockJourneyNode.Setup(x => x.MakeListOfJourneyNodes(
                It.IsAny<List<IJourney>>(), mockDb.Object)).Returns(Task.FromResult(journeysList));
            //
            JourneyController controller = new(mockJService.Object,
                mockVService.Object, mockDb.Object, mockJourneyNode.Object);
            var result =(await controller.GetAllJourneys()).Value;
            //
            Assert.NotNull(result);
            Assert.Equal(journeysList.Count,result.Count);
        }

    }
}
