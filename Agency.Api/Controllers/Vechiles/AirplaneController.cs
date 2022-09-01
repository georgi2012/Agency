using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers.Vechiles
{
    [ApiController]
    [Route("[controller]")]
    public class AirplaneController : Controller
    {
        private readonly IAirplaneService _ariplaneService;
        private readonly IVehicleService _vehicleService;
        public AirplaneController(IAirplaneService airplaneService, IVehicleService vehicleService)
        {
            _ariplaneService = airplaneService;
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<ActionResult> AddAirplane(AirplaneReceiveNode data)
        {
            try
            {
                await _ariplaneService.CreateAirplaneAsync(data.PassangerCapacity, data.PricePerKilometer,
                    data.HasFreeFood);
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
            return Ok($"Vehicle created.");
        }


        [HttpPut]
        public async Task<ActionResult> EditAirplane(AirplaneReceiveNode data)
        {
            try
            {
                IVehicle obj = await _vehicleService.GetVehicleWithIdAsync(data.ID);

                if (obj == null)
                {
                    return NotFound($"Fail. Airplane with such ID not found");
                }
                var plane = obj as Airplane;
                plane.PricePerKilometer = data.PricePerKilometer;
                plane.PassangerCapacity = data.PassangerCapacity;
                plane.HasFreeFood = data.HasFreeFood;

                return Ok("Vehicle changed");
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
        }


    }
}
