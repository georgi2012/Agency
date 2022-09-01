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
            AgencyDBContext inmDbContext = AgencyUtils.InMemoryEmptyContextGenerator();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            Mock<JourneyService> mockJourneyService = new(inmDbContext, mockTicketService.Object);
            int vehID = 42;
            //execution and verification
            var service = new VehicleService(inmDbContext,mockJourneyService.Object);
            Assert.ThrowsAsync<Exception>(async () => await service.RemoveVehicleAsync(vehID));
        }

        [Fact]
        public async Task RemoveVehicle_ShouldRemoveVehicleWhenFoundAndKeepOtherVehicleUntouched()
        {
            //define
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            Mock<TicketService> mockTicketService = new(inmDbContext);
            Mock<JourneyService> mockJourneyService = new(inmDbContext, mockTicketService.Object);
            var vehiclesListBefore = inmDbContext.Vehicles.ToList();
            Assert.True(vehiclesListBefore.Count >= 2);
            var vehIdToRemove = vehiclesListBefore.First().VehicleID;
            var service = new VehicleService(inmDbContext,mockJourneyService.Object);
            mockJourneyService.Setup(x => x.GetJourneyAsync()).
                Returns(Task.FromResult(new List<IJourney>()));
                
            //execute
            await service.RemoveVehicleAsync(vehIdToRemove);
            //verify
            var vehListAfter = inmDbContext.Vehicles.ToList();
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
