using Agency.Api.Controllers.Vechiles;
using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Agency.UnitTests.Agency.Api.Tests.VehicleControllers.BoatControllers
{
    public class AddBoat_Should
    {
        [Fact]
        public async void AddBoat_ShouldReturnOKWhenIsCreatedSuccessfully()
        {
            //arrange
            AgencyDBContext Db = AgencyUtils.InMemorySeededContextGenerator();
            TicketService tService = new(Db);
            JourneyService jService = new(Db, tService);
            VehicleService vService = new(Db, jService);
            BoatService planeService = new(Db);
            BoatReceiveNode node = new BoatReceiveNode();
            //act
            BoatController controller = new(planeService,
                    vService);
            var result = await controller.AddBoat(node);
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }
    }
}
