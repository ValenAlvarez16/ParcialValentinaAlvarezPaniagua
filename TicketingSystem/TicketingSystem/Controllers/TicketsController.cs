using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using TicketingSystem.DAL;
using TicketingSystem.DAL.Entities;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;


namespace TicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : Controller
    {
        private readonly ConcertDB_ValentinaAlvarez _context;

        public TicketsController(ConcertDB_ValentinaAlvarez context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            var ticket = await _context.Tickets.ToListAsync(); // Select * From Tickets

            if (ticket == null) return NotFound();

            return ticket;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid? id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(c => c.Id == id); //Select * From Tickets Where Id = "..."

            if (ticket == null) return NotFound();

            return Ok(ticket);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateTicket(Ticket ticket)
        {
            try
            {
                ticket.Id = Guid.NewGuid();
                ticket.CreatedDate = DateTime.Now;

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync(); //  Insert Into...
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(ticket);
        }


        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditTicket(Guid? id, Ticket ticket)
        {
            try
            {
                if (id != ticket.Id) return NotFound("ticket not found");

                ticket.ModifiedDate = DateTime.Now;

                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync(); // Update...
            }
           
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(ticket);
        }


        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteTicket(Guid? id)
        {
            if (_context.Tickets == null) return Problem("Entity set 'DataBaseContext.Countries' is null.");
            var ticket = await _context.Tickets.FirstOrDefaultAsync(c => c.Id == id);

            if (ticket == null) return NotFound("ticket not found");

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync(); 

            return Ok(new ObjectResult(ticket)
            {
                Value = "El mensaje fue eliminado"
            });
        }

        [HttpPost]
        [Route("Validate")]
        public async Task<ActionResult> ValidateTicket(Guid? id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound("The Ticket is not valid");
            }

            if (ticket.Id != id)
            {
                return BadRequest("The ticket ID does not match");
            }

            if (ticket.IsUsed)
            {
                return Conflict($"The ticket has already been used and its date of use: {ticket.UseDate}. Portería: {ticket.EntranceGate}");
            }

            ticket.UseDate = DateTime.Now;
            ticket.IsUsed = true;
            ticket.EntranceGate = "Occidental";

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();

            return Ok("The ticket is correct");
        }



    }
}
