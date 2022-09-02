using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Api.Controllers.Journey;
using Agency.Api.Controllers.Ticket;
using Agency.Api.Controllers.Vechiles;
using Agency.Api.DTOModels.Journey;
using Agency.Api.DTOModels.Ticket;
using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agency.UnitTests.Agency.Api.Tests.VehicleControllers.VehiclesController
{
    public class GetAllVehs_Should
    {
        [Fact]
        public async void GetAllVehs_ShouldReturnListOfAllAvailableJourneys()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            VehicleNode vehicleNode = new();
            //act
            var vehsList = Db.Vehicles.ToList();
            VehicleController controller = new(vService,vehicleNode);
            var result = (await controller.GetAllVehs()).Value;
            //assert
            Assert.NotNull(result);
            Assert.Equal(vehsList.Count, result.Count);
        }

    }
}
