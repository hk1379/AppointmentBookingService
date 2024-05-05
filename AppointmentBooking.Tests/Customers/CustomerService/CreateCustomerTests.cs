namespace AppointmentBooking.Tests.Customers.CustomerService;

using AppointmentBooking.Customers;
using AppointmentBooking.Customers.Requests;
using AutoFixture;
using AutoFixture.Xunit2;

public class CreateCustomerTests : BaseTest
{
    private readonly ICustomerService _customerService;

    public CreateCustomerTests()
    {
        _customerService = fixture.Create<CustomerService>();
    }

    [Theory, AutoData]
    public void CreateCustomer_ValidCustomerRequestPassedIn_ReturnsTrue(string name, string phoneNumber, string emailAddress, string? companyName)
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
        bool customerCreated = _customerService.CreateCustomer(createCustomerRequest);

        // assert
        Assert.True(customerCreated);
    }

    //[Theory, AutoData]
    //public async Task CreateCustomer_DbSaveFails_ReturnsFalse(string name, string phoneNumber, string emailAddress, string? companyName)
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
    public void CreateCustomer_InvalidCustomerRequestPassedIn_ReturnsArgumentException(string name, string phoneNumber, string emailAddress, string? companyName)
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
        Assert.Throws<ArgumentException>(() => _customerService.CreateCustomer(createCustomerRequest));
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("testName", null)]
    [InlineAutoData("testName", "testPhoneNumber", null)]
    public void CreateCustomer_InvalidCustomerRequestPassedIn_ReturnsArgumentNullException(string name, string phoneNumber, string emailAddress, string? companyName)
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
        Assert.Throws<ArgumentNullException>(() => _customerService.CreateCustomer(createCustomerRequest));
    }

    [Fact]
    public void CreateCustomer_NullCreateCustomerRequestPassedIn_ReturnsArgumentNullException()
    {
        // act and assert
        Assert.Throws<ArgumentNullException>(() => _customerService.CreateCustomer(null));
    }
}
