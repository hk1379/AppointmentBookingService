namespace AppointmentBooking.Customers
{
    using AppointmentBooking.Context;
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Customers.DTO;
    using Microsoft.EntityFrameworkCore;
    using static AppointmentBooking.Customers.ICustomerService;

    public class CustomerService : ICustomerService
    {
        private readonly ILogger<ICustomerService> _logger;
        private readonly AppointmentBookingDbContext _context;

        public CustomerService(
            ILogger<ICustomerService> logger,
            AppointmentBookingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Customer?> GetCustomerAsync(int id) =>
            await _context.Customers.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);

        public async Task<Customer?> GetCustomerTrackingAsync(int id) =>
            await _context.Customers.SingleOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);

        public async Task<CustomerResponse?> CustomerInfoToDisplayAsync(int id)
        {            
            Customer? customer = await this.GetCustomerAsync(id).ConfigureAwait(false);

            if (customer != null) 
            {
                return new CustomerResponse()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    PhoneNumber = customer.PhoneNumber,
                    EmailAddress = customer.EmailAddress,
                    CompanyName = customer.CompanyName,
                };
            }

            return null;
        }

        public async Task<List<Customer>?> GetCustomersAsync(int[] ids) =>
            await _context.Customers.AsNoTracking().Where(c => ids.Contains(c.Id)).ToListAsync();

        public async Task<List<Customer>?> GetAllCustomersAsync() =>
            await _context.Customers.AsNoTracking().ToListAsync();

        public async Task<bool> CreateCustomerAsync(CreateCustomerRequest request)
        {
            int entriesSaved;

            if (request != null)
            {
                Customer customer = new()
                {
                    Name = request.Name,
                    EmailAddress = request.EmailAddress,
                    PhoneNumber = request.PhoneNumber,
                    CompanyName = request.CompanyName,
                };

                _context.Customers.Add(customer);
                entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                _logger.LogError("One of the following are null, Name: {name}, Email Address: {email} or Phone Number: {phoneNumber}",
                                 request?.Name,
                                 request?.EmailAddress,
                                 request?.PhoneNumber);

                throw new ArgumentNullException(nameof(request));
            }

            bool isCustomerSaved = entriesSaved >= 1;
            return isCustomerSaved;
        }

        public async Task<bool> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            Customer? customer = await this.GetCustomerTrackingAsync(request.Id).ConfigureAwait(false);

            if (customer != null)
            {
                customer.Id = request.Id;
                customer.Name = request.Name;
                customer.PhoneNumber = request.PhoneNumber;
                customer.EmailAddress = request.EmailAddress;
                customer.CompanyName = request.CompanyName;
            }

            int entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            return entriesSaved >= 1;
        }

        public async Task<bool> DeleteCustomerAsync(int Id)
        {
            Customer? customer = await this.GetCustomerAsync(Id).ConfigureAwait(false);

            if (customer != null)
            {
                _context.Remove(customer);
                int entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);

                return entriesSaved >= 1;
            }
            
            return false;
        }
    }
}
