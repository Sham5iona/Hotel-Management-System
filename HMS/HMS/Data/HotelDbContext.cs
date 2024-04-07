using HMS.Model;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data
{
    public class HotelDbContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public HotelDbContext(DbContextOptions<HotelDbContext> options) :
            base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasOne(res => res.Customer)
                .WithMany(c => c.Reservations);

            modelBuilder.Entity<Room>().HasMany(r => r.Reservations)
                .WithOne(res => res.Room);

            modelBuilder.Entity<Room>().HasIndex(r => r.RoomNumber)
                .IsUnique();

            modelBuilder.Entity<Customer>().HasIndex(c => c.CustomerEmail)
                .IsUnique();

        }
    }
}
