namespace AppointmentBooking.Tests.Customers.CustomerService;

using AppointmentBooking.Context.Models;
using AppointmentBooking.Customers;
using AppointmentBooking.Customers.Requests;
using AutoFixture;
using AutoFixture.Xunit2;

public class UpdateCustomerAsyncTests : BaseTest
{
    private readonly ICustomerService _customerService;

    public UpdateCustomerAsyncTests()
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
    [InlineAutoData(1)]
    public async Task UpdateCustomerAsync_DifferentCustomerDetailsGiven_ReturnsTrue(int id, string name, string phoneNumber, string emailAddress, string companyName)
    {
        // arrange
        CreateAndSaveCustomer(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());

        UpdateCustomerRequest updateCustomerRequest = fixture
            .Build<UpdateCustomerRequest>()
            .With(c => c.Id, id)
            .With(c => c.Name, name)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        // act
        bool customerUpdated = await _customerService
            .UpdateCustomerAsync(updateCustomerRequest)
            .ConfigureAwait(false);

        // assert
        Assert.True(customerUpdated);
    }

    [Theory]
    [InlineAutoData(1)]
    public async Task UpdateCustomerAsync_OneCustomerDetailsChanged_ReturnsTrue(int id, string name, string phoneNumber, string emailAddress, string companyName)
    {
        // arrange
        CreateAndSaveCustomer(fixture.Create<string>(), phoneNumber, emailAddress, companyName);

        UpdateCustomerRequest updateCustomerRequest = fixture
            .Build<UpdateCustomerRequest>()
            .With(c => c.Id, id)
            .With(c => c.Name, name)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        // act
        bool customerUpdated = await _customerService
            .UpdateCustomerAsync(updateCustomerRequest)
            .ConfigureAwait(false);

        // assert
        Assert.True(customerUpdated);
    }

    [Theory]
    [InlineAutoData(100)]
    public async Task UpdateCustomerAsync_WrongCustomerIdGiven_ReturnsFalse(int id, string name, string phoneNumber, string emailAddress, string companyName)
    {
        // arrange
        CreateAndSaveCustomer(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());

        UpdateCustomerRequest updateCustomerRequest = fixture
            .Build<UpdateCustomerRequest>()
            .With(c => c.Id, id)
            .With(c => c.Name, name)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        // act
        bool customerUpdated = await _customerService
            .UpdateCustomerAsync(updateCustomerRequest)
            .ConfigureAwait(false);

        // assert
        Assert.False(customerUpdated);
    }

    [Theory]
    [InlineAutoData(1)]
    public async Task UpdateCustomerAsync_SameCustomerDetailsGiven_ReturnsFalse(int id, string name, string phoneNumber, string emailAddress, string companyName)
    {
        // arrange
        CreateAndSaveCustomer(name, phoneNumber, emailAddress, companyName);

        UpdateCustomerRequest updateCustomerRequest = fixture
            .Build<UpdateCustomerRequest>()
            .With(c => c.Id, id)
            .With(c => c.Name, name)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        // act
        bool customerUpdated = await _customerService
            .UpdateCustomerAsync(updateCustomerRequest)
            .ConfigureAwait(false);

        // assert
        Assert.True(customerUpdated);
    }

    [Fact]
    public async Task UpdateCustomerAsync_NullUpdateCustomerRequestPassedIn_ReturnsArgumentNullException()
    {
        // act and assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _customerService.UpdateCustomerAsync(null));
    }
}
