using Agency.Core.Contracts;
using Agency.Data.Models.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;
using Agency.Core.Services.VehicleServices;

namespace Agency.UnitTests.Agency.Core.Tests.VehiclesServices.Test.TrainServices
{
    public class CreateTrain_Should
    {
        [Fact]
        public async Task CreateTrain_ShouldAddNewTrainWithCorrectData()
        {
            //mocking
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            int capacity = DataRestrictions.MaxTrainCarts;
            decimal price = DataRestrictions.MaxPricePerKm;
            int carts = DataRestrictions.MaxTrainCarts;
            int oldListCount = inmDbContext.Vehicles.ToList().Count;
            //execution
            var service = new TrainService(inmDbContext);
            await service.CreateTrainAsync(capacity, price, carts);
            //virification
            var vehiclesList = inmDbContext.Vehicles.ToList();
            Assert.Equal(oldListCount + 1, vehiclesList.Count);
            var plane = (Train)vehiclesList.FindLast(x => x is Train &&
                x.PassangerCapacity == capacity && x.PricePerKilometer == price);
            Assert.NotNull(plane);
            Assert.Equal(carts, plane.Carts);
        }
    }
}
