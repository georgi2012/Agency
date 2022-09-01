using Agency.Data.Models.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;
using Microsoft.EntityFrameworkCore;
using Agency.Core.Services.TicketServices;
using Agency.Data.Models.Vehicles.Models;
using Agency.Core.Services.VehicleServices;
using Agency.Core.Services.JourneyServices;
using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.UnitTests.Agency.Core.Tests.VehiclesServices.Test.VehiclesService
{
    public class GetVehicleWithId_Should
    {

        [Fact]
        public async Task GetVehicleWithId_ShouldReturnNullWhenNotFound()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            Mock<JourneyService> mockJourneyService = new(inmDbContext, mockTicketService.Object);
            List<Vehicle> expectedVehiclesList = inmDbContext.Vehicles.ToList();
            //execution
            var service = new VehicleService(inmDbContext,mockJourneyService.Object);
            var returnedVehicle = await service.GetVehicleWithIdAsync(new Random().Next(10,1000));
            //virification
            Assert.Null(returnedVehicle);
        }

        [Fact]
        public async Task GetVehicleWithId_ShouldReturnTheCorrectVehicle()
        {
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            Mock<JourneyService> mockJourneyService = new(inmDbContext, mockTicketService.Object);
            //execution
            var service = new VehicleService(inmDbContext, mockJourneyService.Object);
            List<IVehicle> returnedVehiclesList = await service.GetVehiclesAsync();
            //virification
            Assert.NotNull(returnedVehiclesList);
            Assert.True(returnedVehiclesList.Count == 0);


        }
    }
}