using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers.Vechiles
{
    [ApiController]
    [Route("[controller]")]
    public class TrainController : Controller
    {
        private readonly ITrainService _trainService;
        private readonly IVehicleService _vehicleService;

        public TrainController(ITrainService trainService, IVehicleService vehicleService)
        {
            _trainService = trainService;
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<ActionResult> AddTrain(TrainReceiveNode data)
        {
            try
            {
                await _trainService.CreateTrainAsync(data.PassangerCapacity, data.PricePerKilometer,
                    data.Carts);
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
            return Ok($"Vehicle created.");
        }

        [HttpPut]
        public async Task<ActionResult> EditTrain(TrainReceiveNode data)
        {
            try
            {
                IVehicle obj = await _vehicleService.GetVehicleWithIdAsync(data.ID);

                if (obj == null)
                {
                    return NotFound($"Fail. Airplane with such ID not found");
                }
                var train = obj as Train;
                train.PricePerKilometer = data.PricePerKilometer;
                train.Carts = data.Carts;
                train.PassangerCapacity = data.PassangerCapacity;


                return Ok("Vehicle changed");
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
        }


    }
}
