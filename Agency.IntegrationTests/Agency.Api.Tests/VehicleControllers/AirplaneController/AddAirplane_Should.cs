using Agency.Api.Controllers.Vechiles;
using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agency.UnitTests.Agency.Api.Tests.VehicleControllers.AirplaneControllers
{
    public class AddAirplane_Should
    {
        [Fact]
        public async void AddAirplane_ShouldReturnOKWhenIsCreatedSuccessfully()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            AirplaneService planeService = new(Db);
            AirplaneReceiveNode node = new AirplaneReceiveNode();
            //act
            AirplaneController controller = new(planeService,
                    vService);
            var result = await controller.AddAirplane(node);
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
