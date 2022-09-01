using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Api.Controllers.Journey;
using Agency.Api.Controllers.Ticket;
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

namespace Agency.UnitTests.Agency.Api.Tests.TicketControllers
{
    public class GetAllTickets_Should
    {
        [Fact]
        public async void GetAllTickets_ShouldReturnListOfAllAvailableJourneys()
        {
            //arrange
            Mock<TicketNode> mockT1 = new();
            Mock<TicketNode> mockT2 = new();
            Mock<TicketNode> mockT3 = new();
            List<TicketNode> ticketsList = new() { mockT1.Object, mockT2.Object, mockT3.Object };
            Mock<AgencyDBContext> mockDb = new();
            Mock<TicketService> mockTService = new(mockDb.Object);
            Mock<JourneyService> mockJService = new(mockDb.Object, mockTService.Object);
            Mock<VehicleService> mockVService = new(mockDb.Object, mockJService.Object);
            Mock<ITicketNode> mockTicketNode = new();
            mockTicketNode.Setup(x => x.MakeListOfNodes(
                It.IsAny<List<ITicket>>(), mockDb.Object)).Returns(Task.FromResult(ticketsList));
            //act
            TicketController controller = new(mockJService.Object,
                mockTService.Object, mockDb.Object, mockTicketNode.Object);
            var result = (await controller.GetAllTickets()).Value;
            //assert
            Assert.NotNull(result);
            Assert.Equal(ticketsList.Count, result.Count);
        }

    }
}
