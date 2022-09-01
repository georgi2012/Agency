using Agency.Api.DTOModels.Journey;
using Agency.Core.Contracts;
using Agency.Data.DB;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers.Journey
{
    [ApiController]
    [Route("[controller]")]
    public class JourneyController : Controller
    {
        private readonly IJourneyService _journeyService;
        private readonly IVehicleService _vehicleService;
        private readonly AgencyDBContext _dBContext;
        private readonly IJourneyNode _journeyNodeMaker;

        public JourneyController(IJourneyService journeyService, 
            IVehicleService vehicleService, AgencyDBContext dBContext, IJourneyNode journeyNodeMaker)
        {
            _journeyService = journeyService;
            _vehicleService = vehicleService;
            _dBContext = dBContext;
            _journeyNodeMaker = journeyNodeMaker;
        }

        #region Journey

        [HttpGet]
        public async Task<ActionResult<List<JourneyNode>>> GetAllJourneys() =>
          await _journeyNodeMaker.MakeListOfJourneyNodes(await _journeyService.GetJourneyAsync(), _dBContext);


        [HttpGet("{id}")]
        public async Task<ActionResult<JourneyNode>> GetJourney(Guid id)
        {
            var journey = await _journeyService.GetJourneyWithIdAsync(id);
            if (journey == null)
            {
                return NotFound("Journey was not found");
            }
            return new ActionResult<JourneyNode>(await _journeyNodeMaker.MakeJourneyNode(journey, _dBContext));
        }

        [HttpPost]
        public async Task<ActionResult> AddJourney(JourneyReceiveNode data)
        {
            try
            {
                var veh = await _vehicleService.GetVehicleWithIdAsync(data.VehicleID);
                if(veh == null)
                {
                    return BadRequest($"Fail. Vehicle with such ID could not be found");
                }
                await _journeyService.CreateJourneyAsync(data.StartLocation, data.Destination,
                    data.Distance, veh);
                return Ok("Journey added successfully");
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJourney(Guid id)
        {
            try
            {
                await _journeyService.RemoveJourneyAsync(id);
            }
            catch (Exception e)
            {
                return NotFound("Fail." + e.Message);
            }
            return Ok("Journey removed");
        }


        #endregion

    }
}
