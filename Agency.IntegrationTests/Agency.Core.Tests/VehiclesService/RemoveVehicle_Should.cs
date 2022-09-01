using Moq;
using Agency.Data.DB;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.Models.Contracts;

namespace Agency.UnitTests.Agency.Core.Tests.VehiclesServices.Test.VehiclesService
{
    public class RemoveVehicle_Should
    {
        [Fact]
        public async Task RemoveVehicle_ShouldThrowWhenNotFound()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            TicketService ticketService = new(inmDbContext);
            JourneyService journeyService = new(inmDbContext, ticketService);
            //act and assert
            var service = new VehicleService(inmDbContext,journeyService);
            Assert.ThrowsAsync<Exception>(async () => await service.RemoveVehicleAsync(42));
        }

        [Fact]
        public async Task RemoveVehicle_ShouldRemoveVehicleWhenFoundAndKeepOtherVehicleUntouched()
        {
            //arrange
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            TicketService ticketService = new(inmDbContext);
            JourneyService journeyService = new(inmDbContext, ticketService);
            var vehiclesListBefore = inmDbContext.Vehicles.ToList();
            Assert.True(vehiclesListBefore.Count >= 2);
            var vehIdToRemove = vehiclesListBefore.First().VehicleID;
            var service = new VehicleService(inmDbContext,journeyService);
            //act
            await service.RemoveVehicleAsync(vehIdToRemove);
            var vehListAfter = inmDbContext.Vehicles.ToList();
            //assert
            Assert.NotNull(vehListAfter);
            Assert.Equal(vehiclesListBefore.Count - 1, vehListAfter.Count);
            foreach (var ticket in vehListAfter)
            {
                Assert.True(vehiclesListBefore.Find(x => x.VehicleID == ticket.VehicleID) != null);
            }
            Assert.True(vehListAfter.Find(x => x.VehicleID == vehIdToRemove) == null);
        }
    }
}
