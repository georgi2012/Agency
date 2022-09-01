using Agency.Api.DTOModels.Vehicle;
using Agency.Api.Models;
using Agency.Core.Contracts;
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Agency.Api.Controllers.Vechiles
{

    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleNode _vehicleNodeMaker;

        public VehicleController(IVehicleService vehicleService, IVehicleNode vehicleNodeMaker)
        {
            this._vehicleService = vehicleService;
            _vehicleNodeMaker = vehicleNodeMaker;
        }


        [HttpGet("Types")]
        public async Task<ActionResult<List<StringModel>>> GetTypes()
        {
            return StringModel.CreateListOfModels(new List<string>
            {
                "Bus",
                "Airplane",
                "Train",
                "Boat"
            });
        }


        [HttpGet]
        public virtual async Task<ActionResult<List<VehicleNode>>> GetAllVehs() => await
            _vehicleNodeMaker.MakeNodeListFromVehiclesList(await _vehicleService.GetVehiclesAsync());


        [HttpGet("{id}")]
        public virtual async Task<ActionResult<VehicleNode>> GetVeh(int id)
        {
            var veh = await _vehicleService.GetVehicleWithIdAsync(id);
            if (veh == null)
            {
                return NotFound("Vehicle was not found");
            }
            return new ActionResult<VehicleNode>(await _vehicleNodeMaker.MakeNodeFromVehicle(veh));
        }


        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> DeleteVeh(int id)
        {
            try
            {
                await _vehicleService.RemoveVehicleAsync(id);
            }
            catch (Exception e)
            {
                return NotFound("Fail." + e.Message);
            }
            return Ok("Vehicle deleted");
        }


    }
}
