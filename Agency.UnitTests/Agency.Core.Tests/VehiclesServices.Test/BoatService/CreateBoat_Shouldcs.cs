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

namespace Agency.UnitTests.Agency.Core.Tests.VehiclesServices.Test.BoatServices
{
    public class CreateBoat_Shouldcs
    {
        [Fact]
        public async Task CreateBoat_ShouldAddNewBoatWithCorrectData()
        {
            //mocking
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            int capacity = DataRestrictions.MaxBoatCapacity;
            decimal price = DataRestrictions.MaxPricePerKm;
            bool hasWater = true;
            int oldListCount = inmDbContext.Vehicles.ToList().Count;
            //execution
            var service = new BoatService(inmDbContext);
            await service.CreateBoatAsync(capacity, price, hasWater);
            //virification
            var vehiclesList = inmDbContext.Vehicles.ToList();
            Assert.Equal(oldListCount + 1, vehiclesList.Count);
            var plane = (Boat)vehiclesList.FindLast(x => x is Boat &&
                x.PassangerCapacity == capacity && x.PricePerKilometer == price);
            Assert.NotNull(plane);
            Assert.Equal(hasWater, plane.OffersWaterSports);
        }
    }
}
