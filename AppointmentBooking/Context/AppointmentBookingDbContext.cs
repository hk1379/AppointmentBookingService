using AppointmentBooking.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Context
{
    public class AppointmentBookingDbContext : DbContext
    {
        public AppointmentBookingDbContext(DbContextOptions<AppointmentBookingDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Meetings)
                .WithMany(m => m.Customers);
                //.UsingEntity(
                //    l => l.HasOne(typeof(Meeting)).WithMany().HasForeignKey("CustomerId"),
                //    r => r.HasOne(typeof(Customer)).WithMany().HasForeignKey("MeetingId")
                //);
        }
    }
}
