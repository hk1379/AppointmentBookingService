namespace AppointmentBooking.Tests.Customers.CustomerService;

using AppointmentBooking.Context.Models;
using AppointmentBooking.Customers;
using AppointmentBooking.Customers.Responses;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;

public class CustomerInfoToDisplayTests : BaseTest
{
    private readonly ICustomerService _customerService;

    public CustomerInfoToDisplayTests()
    {
        _customerService = fixture.Create<CustomerService>();
    }

    [Theory, AutoData]
    public async Task CustomerInfoToDisplayAsync_KnownCustomerIdPassedIn_ReturnsCustomerObject(int customerId, string customerName, string phoneNumber, string emailAddress, string companyName)
    {
        // arrange
        Customer customer = fixture
            .Build<Customer>()
            .Without(c => c.Meetings)
            .With(c => c.Id, customerId)
            .With(c => c.Name, customerName)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        context.Add(customer);
        context.SaveChanges();

        // act
        CustomerResponse? customerResponse = await _customerService
            .CustomerInfoToDisplayAsync(customerId)
            .ConfigureAwait(false);

        // assert
        Assert.Equal(customerId, customerResponse?.Id);
        Assert.Equal(customerName, customerResponse?.Name);
        Assert.Equal(phoneNumber, customerResponse?.PhoneNumber);
        Assert.Equal(emailAddress, customerResponse?.EmailAddress);
        Assert.Equal(companyName, customerResponse?.CompanyName);
    }

    [Theory, AutoData]
    public async Task CustomerInfoToDisplayAsync_UnknownCustomerIdPassedIn_ReturnsNull(int customerId, string customerName, string phoneNumber, string emailAddress, string companyName)
    {
        // arrange
        Customer customer = fixture
            .Build<Customer>()
            .Without(c => c.Meetings)
            .With(c => c.Id, It.IsAny<int>())
            .With(c => c.Name, customerName)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        context.Add(customer);
        context.SaveChanges();

        // act
        CustomerResponse? customerResponse = await _customerService
            .CustomerInfoToDisplayAsync(customerId)
            .ConfigureAwait(false);

        // assert
        Assert.Null(customerResponse);
    }
}
