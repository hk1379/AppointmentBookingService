namespace AppointmentBooking.Tests.Customers.CustomerService;

using AppointmentBooking.Customers;
using AppointmentBooking.Customers.Requests;
using AutoFixture;
using AutoFixture.Xunit2;

public class CreateCustomerAsyncTests : BaseTest
{
    private readonly ICustomerService _customerService;

    public CreateCustomerAsyncTests()
    {
        _customerService = fixture.Create<CustomerService>();
    }

    [Theory, AutoData]
    public async Task CreateCustomerAsync_ValidCustomerRequestPassedIn_ReturnsTrue(string name, string phoneNumber, string emailAddress, string? companyName)
    {
        // arrange
        CreateCustomerRequest createCustomerRequest = fixture
            .Build<CreateCustomerRequest>()
            .With(c => c.Name, name)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        // act
        bool customerCreated = await _customerService
            .CreateCustomerAsync(createCustomerRequest)
            .ConfigureAwait(false);

        // assert
        Assert.True(customerCreated);
    }

    //[Theory, AutoData]
    //public async Task CreateCustomerAsync_DbSaveFails_ReturnsFalse(string name, string phoneNumber, string emailAddress, string? companyName)
    //{
    //    // arrange
    //    CreateCustomerRequest createCustomerRequest = fixture
    //        .Build<CreateCustomerRequest>()
    //        .With(c => c.Name, name)
    //        .With(c => c.PhoneNumber, phoneNumber)
    //        .With(c => c.EmailAddress, emailAddress)
    //        .With(c => c.CompanyName, companyName)
    //        .Create();

    //    await _customerService
    //        .CreateCustomerAsync(createCustomerRequest)
    //        .ConfigureAwait(false);

    //    // act
    //    bool customerCreated = await _customerService
    //        .CreateCustomerAsync(createCustomerRequest)
    //        .ConfigureAwait(false);

    //    // assert
    //    Assert.False(customerCreated);
    //}

    [Theory]
    [InlineAutoData("")]
    [InlineAutoData("testName", "")]
    [InlineAutoData("testName", "testPhoneNumber", "")]
    public async Task CreateCustomerAsync_InvalidCustomerRequestPassedIn_ReturnsArgumentException(string name, string phoneNumber, string emailAddress, string? companyName)
    {
        // arrange
        CreateCustomerRequest createCustomerRequest = fixture
            .Build<CreateCustomerRequest>()
            .With(c => c.Name, name)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        // act and assert
        await Assert.ThrowsAsync<ArgumentException>(() => _customerService.CreateCustomerAsync(createCustomerRequest));
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("testName", null)]
    [InlineAutoData("testName", "testPhoneNumber", null)]
    public async Task CreateCustomerAsync_InvalidCustomerRequestPassedIn_ReturnsArgumentNullException(string name, string phoneNumber, string emailAddress, string? companyName)
    {
        // arrange
        CreateCustomerRequest createCustomerRequest = fixture
            .Build<CreateCustomerRequest>()
            .With(c => c.Name, name)
            .With(c => c.PhoneNumber, phoneNumber)
            .With(c => c.EmailAddress, emailAddress)
            .With(c => c.CompanyName, companyName)
            .Create();

        // act and assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _customerService.CreateCustomerAsync(createCustomerRequest));
    }

    [Fact]
    public async Task CreateCustomerAsync_NullCreateCustomerRequestPassedIn_ReturnsArgumentNullException()
    {
        // act and assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _customerService.CreateCustomerAsync(null));
    }
}
