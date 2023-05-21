using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using TicketingSystem.DAL.Entities;

namespace TicketingSystem.DAL
{
    public class ConcertDB_ValentinaAlvarez : DbContext
    {
        public ConcertDB_ValentinaAlvarez(DbContextOptions<ConcertDB_ValentinaAlvarez> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().HasIndex(c => c.Id).IsUnique();
        }
    }
}   
