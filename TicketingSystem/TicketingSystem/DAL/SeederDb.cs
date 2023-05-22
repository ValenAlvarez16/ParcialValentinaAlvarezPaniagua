
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
            await _context.Database.EnsureCreatedAsync();
            await PopulateTicketsAsync();
            await _context.SaveChangesAsync();
        }


        private async Task PopulateTicketsAsync()
        {
            if (!_context.Tickets.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    _context.Tickets.Add(new Ticket { IsUsed = false, EntranceGate = "Null" });

                }
            }

        }


    }
}
