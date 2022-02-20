using lab_4.Models;
using Microsoft.EntityFrameworkCore;

namespace lab_4.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()    {}

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}

        public DbSet<Passangers> Passangers { get; set; }
        public DbSet<Stations> Stations { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Trains> Trains { get; set; }
        public DbSet<Cars> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>().HasKey(cars => 
                    new { cars.CarId, cars.TrainId });
            modelBuilder.Entity<Passangers>().HasKey(passangers => 
                    new { passangers.PassengerId});
            modelBuilder.Entity<Stations>().HasKey(stations =>
                    new { stations.StationId });
            modelBuilder.Entity<Trains>().HasKey(trains =>
                    new { trains.TrainType });

            modelBuilder.Entity<Tickets>().HasKey(tickets => 
                    new { tickets.TicketId  });

        }

    }
}