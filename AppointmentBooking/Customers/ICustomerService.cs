using AppointmentBooking.Context.Models;
using AppointmentBooking.Customers.DTO;

namespace AppointmentBooking.Customers
{
    public interface ICustomerService
    {
        public record CreateCustomerRequest(string Name, string PhoneNumber, string EmailAddress, string? CompanyName);

        public record UpdateCustomerRequest(int Id, string Name, string PhoneNumber, string EmailAddress, string? CompanyName);

        public Task<Customer?> GetCustomerAsync(int Id);

        public Task<CustomerResponse?> CustomerInfoToDisplayAsync(int customerId);

        public Task<IEnumerable<Customer?>?> GetCustomersAsync();

        public Task<bool> CreateCustomerAsync(CreateCustomerRequest request);

        public Task<bool> UpdateCustomerAsync(UpdateCustomerRequest request);

        public Task<bool> DeleteCustomerAsync(int customerId);
    }
}
