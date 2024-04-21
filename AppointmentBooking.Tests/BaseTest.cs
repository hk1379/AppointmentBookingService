namespace AppointmentBooking.Tests;

using AppointmentBooking.Context;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore;

public abstract class BaseTest
{
    public BaseTest()
    {
        Fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });

        var options = new DbContextOptionsBuilder<AppointmentBookingDbContext>()
            .UseInMemoryDatabase(databaseName: Fixture.Create<string>()).Options;
        Context = new AppointmentBookingDbContext(options);

        Fixture.Inject(Context);
    }

    /// <summary>
    /// Gets the auto fixture instance.
    /// </summary>
    protected IFixture Fixture { get; }
    protected AppointmentBookingDbContext Context { get; }
}