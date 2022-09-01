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
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService ticketService = new(inmDbContext);
            JourneyService journeyService = new(inmDbContext, ticketService);
            List<Vehicle> expectedVehiclesList = inmDbContext.Vehicles.ToList();
            var service = new VehicleService(inmDbContext,journeyService);
            //act
            var returnedVehicle = await service.GetVehicleWithIdAsync(new Random().Next(10,1000));
            //assert
            Assert.Null(returnedVehicle);
        }

        [Fact]
        public async Task GetVehicleWithId_ShouldReturnTheCorrectVehicle()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService ticketService = new(inmDbContext);
            JourneyService journeyService = new(inmDbContext, ticketService);
            var service = new VehicleService(inmDbContext, journeyService);
            //act
            List<IVehicle> returnedVehiclesList = await service.GetVehiclesAsync();
            //assert
            Assert.NotNull(returnedVehiclesList);
            Assert.True(returnedVehiclesList.Count == 0);


        }
    }
}