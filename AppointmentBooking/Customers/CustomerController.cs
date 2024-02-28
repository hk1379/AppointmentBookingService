namespace AppointmentBooking.Customers
{
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Customers.DTO;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using static AppointmentBooking.Customers.ICustomerService;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        // working
        [HttpGet]
        [Route("{customerId}")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute] int customerId)
        {
            try
            {
                CustomerResponse? customer = await _customerService.CustomerInfoToDisplayAsync(customerId).ConfigureAwait(false);

                if (customer != null)
                {
                    return this.Ok(customer);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while searching for customer: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // working
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                IEnumerable<Customer?>? customers = await _customerService.GetCustomersAsync();
                
                if (customers != null)
                {
                    return this.Ok(customers);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while searching for all customers: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // working
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool customerCreated = await _customerService.CreateCustomerAsync(request).ConfigureAwait(false);

                    if (customerCreated == true)
                        return this.Ok("Customer Created");
                    else
                        return this.StatusCode((int) HttpStatusCode.InternalServerError);
                }
                return this.StatusCode((int) HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst creating customer: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        // working
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool customerUpdated = await _customerService.UpdateCustomerAsync(request).ConfigureAwait(false);

                    if (customerUpdated == true)
                        return this.Ok("Customer Updated");
                    else
                        return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }

                return this.StatusCode((int) HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error whilst updating customer: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        // working
        [HttpDelete]
        [Route("{customerId}")]
        public async Task<IActionResult> DeleteCustomerAsync([FromRoute] int customerId)
        {
            try
            {
                bool customerDeleted = await _customerService.DeleteCustomerAsync(customerId).ConfigureAwait(false);

                if (customerDeleted == true)
                    return this.Ok("Customer successfully deleted");
                else
                    return this.NotFound("Customer doesn't exist for the customer id supplied");
            }
            catch (Exception ex)
            {
                _logger.LogError("The following error occured whilst attempting to delete this customer: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
