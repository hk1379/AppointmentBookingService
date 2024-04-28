namespace AppointmentBooking.Tests;

using AppointmentBooking.Context;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore;

public abstract class BaseTest
{
    /// <summary>
    /// Gets the auto fixture instance.
    /// </summary>
    protected readonly IFixture fixture;
    protected readonly AppointmentBookingDbContext context;

    public BaseTest()
    {
        fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });

        var options = new DbContextOptionsBuilder<AppointmentBookingDbContext>()
            .UseInMemoryDatabase(databaseName: fixture.Create<string>()).Options;
        context = new AppointmentBookingDbContext(options);

        fixture.Inject(context);
    }
}