using AppointmentBooking.Context.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Context
{
    public class AppointmentBookingDbContext : IdentityDbContext<IdentityUser>
    {
        public AppointmentBookingDbContext(DbContextOptions<AppointmentBookingDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // many-(customers)-to-many(meetings)
            modelBuilder
                .Entity<Customer>()
                .HasMany(c => c.Meetings)
                .WithMany(m => m.Customers);

            // many-(companies)-to-many(meetings)
            modelBuilder
                .Entity<Company>()
                .HasMany(c => c.Meetings)
                .WithMany(m => m.Companies);

            //modelBuilder
            //    .Entity<Customer>()
            //    .Property(c => c.Meetings)
            //    .ValueGeneratedOnAdd();

            //modelBuilder
            //    .Entity<Company>()
            //    .Property(c => c.Meetings)
            //    .ValueGeneratedOnAdd();

            //modelBuilder
            //    .Entity<Meeting>()
            //    .Property(m => m.Customers)
            //    .ValueGeneratedOnAdd();

            //modelBuilder
            //    .Entity<Meeting>()
            //    .Property(m => m.Companies)
            //    .ValueGeneratedOnAdd();
        }
    }
}
