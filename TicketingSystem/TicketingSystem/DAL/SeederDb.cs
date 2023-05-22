
using TicketingSystem.DAL.Entities;

namespace TicketingSystem.DAL
{
    public class SeederDb
    {
        private readonly ConcertDB_ValentinaAlvarez _context;
        public SeederDb(ConcertDB_ValentinaAlvarez context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //This line helps me create my database automatically
            //await PopulateTicketsAsync();
           

            await _context.SaveChangesAsync();
        }

        private async Task PopulateTicketsAsync()
        {
            for (int i = 0; i < 50000; i++)
            {
                Ticket ticket = new Ticket
                {
                    UseDate = null,
                    IsUsed = false,
                    EntranceGate = null
                };

                _context.Tickets.Add(ticket);
            }
        }


    }
}
