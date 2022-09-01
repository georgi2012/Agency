using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers.Vechiles
{
    [ApiController]
    [Route("[controller]")]
    public class BoatController : Controller
    {

        private readonly IBoatService _boatService;
        private readonly IVehicleService _vehicleService;
        public BoatController(IBoatService boatService, IVehicleService vehicleService)
        {
            _boatService = boatService;
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<ActionResult> AddBoat(BoatReceiveNode data)
        {
            try
            {
                await _boatService.CreateBoatAsync(data.PassangerCapacity, data.PricePerKilometer,
                    data.HasWaterSports);
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
            return Ok($"Vehicle created.");
        }


        [HttpPut]
        public async Task<ActionResult> EditBoat(BoatReceiveNode data)
        {
            try
            {
                IVehicle obj = await _vehicleService.GetVehicleWithIdAsync(data.ID);

                if (obj == null)
                {
                    return NotFound($"Fail. Airplane with such ID not found");
                }
                var boat = obj as Boat;
                boat.PricePerKilometer = data.PricePerKilometer;
                boat.PassangerCapacity = data.PassangerCapacity;
                boat.OffersWaterSports = data.HasWaterSports;

                return Ok("Vehicle changed");
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
        }

    }
}
