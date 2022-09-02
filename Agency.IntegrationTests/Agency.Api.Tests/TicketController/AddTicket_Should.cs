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
    public class AddTicket_Should
    {
        [Fact]
        public async void AddTicket_ShouldReturnBadRequestWhenJourneyIsNotFound()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            VehicleService vService = new(db, jService);
            TicketReceiveNode ticketDTO = new();
            TicketNode tNode = new();
            //act
            TicketController controller = new(jService, tService,
                 db, tNode);
            var result = await controller.AddTicket(ticketDTO);
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);

        }

        [Fact]
        public async void AddTicket_ShouldReturnOKWhenIsCreatedSuccessfully()
        {
            //arrange
            AgencyDBContext db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(db);
            JourneyService jService = new(db, tService);
            VehicleService vService = new(db, jService);
            TicketReceiveNode ticketDTO = new();
            ticketDTO.JourneyID = db.Journeys.ToList().First().JourneyID.ToString();
            TicketNode tNode = new();
            //act
            TicketController controller = new(jService, tService,
                 db, tNode);
            var result = await controller.AddTicket(ticketDTO);
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

    }
}
