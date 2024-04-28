using AppointmentBooking.Context.Models;
using AppointmentBooking.Customers.DTO;

namespace AppointmentBooking.Customers
{
    public interface ICustomerService
    {
        record CreateCustomerRequest(string Name, string PhoneNumber, string EmailAddress, string? CompanyName);

        record UpdateCustomerRequest(int Id, string Name, string PhoneNumber, string EmailAddress, string? CompanyName, int[] MeetingIds);

        Task<Customer?> GetCustomerAsync(int id);

        Task<Customer?> GetCustomerTrackingAsync(int id);

        Task<CustomerResponse?> CustomerInfoToDisplayAsync(int customerId);

        Task<List<Customer>?> GetCustomersAsync(int[] ids);

        Task<List<Customer>?> GetAllCustomersAsync();

        Task<bool> CreateCustomerAsync(CreateCustomerRequest request);

        Task<bool> UpdateCustomerAsync(UpdateCustomerRequest request);

        Task<bool> DeleteCustomerAsync(int customerId);
    }
}
