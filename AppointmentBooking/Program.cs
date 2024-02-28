using AppointmentBooking.Context;
using AppointmentBooking.Customers;
using AppointmentBooking.Meetings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add db context
builder.Services.AddDbContext<AppointmentBookingDbContext>(options => options.UseInMemoryDatabase("AppointmentBooking"));
/** this is proper way
var connectionString = builder.Configuration.GetConnectionString("ApplicationBookingDb");
builder.Services.AddDbContext<AppointmentBookingDbContext>(options => options
    .UseSqlServer(connectionString)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

builder.Services.AddHealthChecks().AddSqlServer(connectionString, "SELECT 1", "AppointBooking");
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
**/

// Add services to the container.
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IMeetingService, MeetingService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
