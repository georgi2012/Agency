using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;


namespace Agency.Api.Controllers.Vechiles
{
    [ApiController]
    [Route("[controller]")]
    public class BusController : Controller
    {
        private readonly IBusService _busService;
        private readonly IVehicleService _vehicleService;

        public BusController(IBusService busService, IVehicleService vehicleService)
        {
            _busService = busService;
            _vehicleService = vehicleService;
        }


        [HttpPost]
        public async Task<ActionResult> AddBus(BusReceiveNode data)
        {
            try
            {
                await _busService.CreateBusAsync(data.PassangerCapacity, data.PricePerKilometer);
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
            return Ok($"Vehicle created.");
        }

        [HttpPut]
        public async Task<ActionResult> EditBus(BusReceiveNode data)
        {
            try
            {
                IVehicle obj = await _vehicleService.GetVehicleWithIdAsync(data.ID);

                if (obj == null)
                {
                    return NotFound($"Fail. Airplane with such ID not found");
                }
                var bus = obj as Bus;
                bus.PricePerKilometer = data.PricePerKilometer;
                bus.PassangerCapacity = data.PassangerCapacity;



                return Ok("Vehicle changed");
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
        }
    }
}
