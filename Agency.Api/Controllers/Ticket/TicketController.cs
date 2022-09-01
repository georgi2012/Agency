using Agency.Api.DTOModels.Ticket;
using Agency.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Agency.Data.DB;

namespace Agency.Api.Controllers.Ticket
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {

        private readonly IJourneyService _jouneyService;
        private readonly ITicketService _ticketService;
        private readonly AgencyDBContext _dBContext;
        private readonly ITicketNode _ticketNodeMaker;

        public TicketController(IJourneyService jouneyService, ITicketService ticketService, 
            AgencyDBContext dBContext, ITicketNode ticketNodeMaker)
        {
            _jouneyService = jouneyService;
            _ticketService = ticketService;
            _dBContext = dBContext;
            _ticketNodeMaker = ticketNodeMaker;
        }


        [HttpGet]
        public async Task<ActionResult<List<TicketNode>>> GetAllTickets() =>
            await _ticketNodeMaker.MakeListOfNodes(await _ticketService.GetTicketsAsync(), _dBContext);


        [HttpGet("{id}")]
        public async Task<ActionResult<TicketNode>> GetTicket(Guid id)
        {
            var ticket = await _ticketService.GetTicketWithIdAsync(id);
            if (ticket == null)
            {
                return NotFound("Ticked not found");
            }
            return new ActionResult<TicketNode>(await _ticketNodeMaker.MakeTicketNode(ticket, _dBContext));
        }

        [HttpPost]
        public async Task<ActionResult> AddTicket(TicketReceiveNode data)
        {
            try
            {
                var journey = await _jouneyService.GetJourneyWithIdAsync(Guid.Parse(data.JourneyID));
                if (journey == null)
                {
                    return BadRequest("Journey with such ID could not be found");
                }
                await _ticketService.CreateTicketAsync(journey, data.AdministrativeCosts);
                return Ok("Ticket was added");
            }
            catch (Exception e)
            {
                return BadRequest($"Fail. {e.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(Guid id)
        {
            try
            {
                await _ticketService.RemoveTicketAsync(id);
            }
            catch (Exception e)
            {
                return NotFound("Fail." + e.Message);
            }
            return Ok("Ticket was removed");
        }




    }
}
