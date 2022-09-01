using Agency.Core.Contracts;
using Agency.Data.Models.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;
using Agency.Core.Services.VehicleServices;

namespace Agency.UnitTests.Agency.Core.Tests.VehiclesServices.Test.BusServices
{
    public class CreateBus_Should
    {
        [Fact]
        public async Task CreateBus_ShouldAddNewBusWithCorrectData()
        {
            //mocking
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            int capacity = DataRestrictions.MaxBusCapacity;
            decimal price = DataRestrictions.MaxPricePerKm;
            int oldListCount = inmDbContext.Vehicles.ToList().Count;
            //execution
            var service = new BusService(inmDbContext);
            await service.CreateBusAsync(capacity, price);
            //virification
            var vehiclesList = inmDbContext.Vehicles.ToList();
            Assert.Equal(oldListCount + 1, vehiclesList.Count);
            var plane = (Bus)vehiclesList.FindLast(x => x is Bus &&
                x.PassangerCapacity == capacity && x.PricePerKilometer == price);
            Assert.NotNull(plane);
        }
    }
}
