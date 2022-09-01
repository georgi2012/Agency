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

namespace Agency.UnitTests.Agency.Core.Tests.VehiclesServices.Test.AirplanesService
{
    public class CreateAirplane_Should
    {
        [Fact] 
        public async Task CreateAirplane_ShouldAddNewAirplaneWithCorrectData()
        {
            //mocking
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            int capacity = DataRestrictions.MaxAirplaneCapacity;
            decimal price = DataRestrictions.MaxPricePerKm;
            bool hasFood = true;
            int oldListCount = inmDbContext.Vehicles.ToList().Count;
            //execution
            var service = new AirplaneService(inmDbContext);
            await service.CreateAirplaneAsync(capacity,price,hasFood);
            //virification
            var vehiclesList = inmDbContext.Vehicles.ToList();
            Assert.Equal(oldListCount + 1, vehiclesList.Count);
            Airplane plane = (Airplane)vehiclesList.FindLast(x => x is Airplane && 
                x.PassangerCapacity == capacity && x.PricePerKilometer == price);
            Assert.NotNull(plane);
            Assert.Equal(hasFood, plane.HasFreeFood);
        }

    }
}
