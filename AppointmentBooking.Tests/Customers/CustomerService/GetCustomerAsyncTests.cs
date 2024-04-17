using AppointmentBooking.Customers;
using AppointmentBooking.Customers.DTO;

namespace AppointmentBooking.Tests.Customers.CustomerService;

public class GetCustomerAsyncTests
{
    private readonly ICustomerService _customerService;

    public GetCustomerAsyncTests(ICustomerService customerService)
    {
        _customerService = this.Fixture.Create
    }

    [Theory]
    [InlineData(3)]
    public async Task CustomerInfoToDisplayAsync_KnownCustomerIdPassedIn_ReturnsCustomerObjectAsync(int customerId)
    {
        // arrange

        // act
        CustomerResponse? customerResponse = await _customerService
            .CustomerInfoToDisplayAsync(customerId)
            .ConfigureAwait(false);

        // assert
    }
}
