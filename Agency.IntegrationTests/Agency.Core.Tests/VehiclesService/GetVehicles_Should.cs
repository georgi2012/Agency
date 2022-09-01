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
    public class GetVehicles_Should
    {
        [Fact]
        public async Task GetVehicles_ShouldReturnAllVehiclesIfAnyAreAvailable()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService ticketService = new(inmDbContext);
            JourneyService journeyService = new(inmDbContext, ticketService);
            List<IVehicle> returnedVehiclesList = null;
            List<Vehicle> expectedVehiclesList = inmDbContext.Vehicles.ToList();
            var service = new VehicleService(inmDbContext,journeyService);
            //act
            returnedVehiclesList = await service.GetVehiclesAsync();
            //assert
            Assert.NotNull(returnedVehiclesList);
            Assert.Equal(expectedVehiclesList.Count, returnedVehiclesList.Count);

            for (int i = 0; i < returnedVehiclesList.Count; i++)
            {
                Assert.Equal(expectedVehiclesList[i], returnedVehiclesList[i]);
            }
        }

        [Fact]
        public async Task GetVehicles_ShouldReturnEmptyListIfDbIsEmpty()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService ticketService = new(inmDbContext);
            Mock<JourneyService> mockJourneyService = new(inmDbContext,ticketService);
            var service = new VehicleService(inmDbContext,mockJourneyService.Object);
            //act
            List<IVehicle> returnedVehiclesList = await service.GetVehiclesAsync();
            //assert
            Assert.NotNull(returnedVehiclesList);
            Assert.True(returnedVehiclesList.Count == 0);
        }

    }
}
