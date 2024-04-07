using AppointmentBooking.Companies;
using AppointmentBooking.Context;
using AppointmentBooking.Customers;
using AppointmentBooking.Meetings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add db context
// builder.Services.AddDbContext<AppointmentBookingDbContext>(options => options.UseInMemoryDatabase("AppointmentBooking"));
var connectionstring = builder.Configuration.GetConnectionString("applicationbookingdb");

builder.Services.AddDbContext<AppointmentBookingDbContext>(options => options
    .UseSqlServer(connectionstring)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

//builder.Services.AddHealthChecks().AddSqlServer(connectionString, "SELECT 1", "AppointBooking");
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IMeetingService, MeetingService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ignore dependency cycles which is giving errors due to circular relationship between customers and meetings
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//    });

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
