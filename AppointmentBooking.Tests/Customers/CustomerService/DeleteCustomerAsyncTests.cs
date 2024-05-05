namespace AppointmentBooking.Tests.Customers.CustomerService;

using AppointmentBooking.Context.Models;
using AppointmentBooking.Customers.Requests;
using AppointmentBooking.Customers;
using AutoFixture.Xunit2;
using AutoFixture;
public class DeleteCustomerAsyncTests : BaseTest
{
    private readonly ICustomerService _customerService;

    public DeleteCustomerAsyncTests()
    {
        _customerService = fixture.Create<CustomerService>();
    }

    private void CreateAndSaveCustomer(string name, string phoneNumber, string emailAddress, string companyName)
    {
        Customer customer = new()
        {
            Name = name,
            EmailAddress = phoneNumber,
            PhoneNumber = emailAddress,
            CompanyName = companyName,
        };
        context.Add(customer);
        context.SaveChanges();
    }

    [Theory]
    [InlineData(1)]
    public async Task DeleteCustomerAsync_ValidCustomerIdGiven_ReturnsTrue(int id)
    {
        // arrange
        CreateAndSaveCustomer(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());

        // act
        bool customerDeleted = await _customerService
            .DeleteCustomerAsync(id)
            .ConfigureAwait(false);

        // assert
        Assert.True(customerDeleted);
    }

    [Theory]
    [InlineData(100)]
    public async Task DeleteCustomerAsync_UnknownCustomerIdGiven_ReturnsFalse(int id)
    {
        // arrange
        CreateAndSaveCustomer(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());

        // act
        bool customerDeleted = await _customerService
            .DeleteCustomerAsync(id)
            .ConfigureAwait(false);

        // assert
        Assert.False(customerDeleted);
    }
}
