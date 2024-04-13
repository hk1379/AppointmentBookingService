using AppointmentBooking.Companies;
using AppointmentBooking.Context;
using AppointmentBooking.Customers;
using AppointmentBooking.Meetings;
using Microsoft.AspNetCore.Identity;
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

// Configuring Microsoft Identity
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Default Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // Default Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Default SignIn settings.
//    options.SignIn.RequireConfirmedEmail = false;
//    options.SignIn.RequireConfirmedPhoneNumber = false;

//    // Default User settings.
//    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});

// Identity with a default UI
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppointmentBookingDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<IdentityUser, AppointmentBookingDbContext>();

// Add services to the container.
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
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
