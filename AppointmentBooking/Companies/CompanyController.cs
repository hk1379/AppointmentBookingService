using Microsoft.AspNetCore.Mvc;
using System.Net;
using AppointmentBooking.Companies.DTO;
using static AppointmentBooking.Companies.ICompanyService;
using AppointmentBooking.Context.Models;

namespace AppointmentBooking.Companies
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;

        private readonly ICompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet]
        [Route("{companyId}")]
        public async Task<IActionResult> GetCompanyAsync([FromRoute] int companyId)
        {
            try
            {
                CompanyResponse? company = await _companyService.CompanyInfoToDisplayAsync(companyId).ConfigureAwait(false);

                if (company != null)
                {
                    return this.Ok(company);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while searching for company: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                IEnumerable<Company?>? companies = await _companyService.GetAllCompaniesAsync();

                if (companies != null)
                {
                    return this.Ok(companies);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while searching for all companies: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CreateCompanyRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool companyCreated = await _companyService.CreateCompanyAsync(request).ConfigureAwait(false);

                    if (companyCreated == true)
                        return this.Ok("Company Created");
                    else
                        return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst creating company: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCompanyAsync([FromBody] UpdateCompanyRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool companyUpdated = await _companyService.UpdateCompanyAsync(request).ConfigureAwait(false);

                    if (companyUpdated == true)
                        return this.Ok("Company Updated");
                    else
                        return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst updating company: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{companyId}")]
        public async Task<IActionResult> DeleteCompanyAsync([FromRoute] int companyId)
        {
            try
            {
                bool companyDeleted = await _companyService.DeleteCompanyAsync(companyId).ConfigureAwait(false);

                if (companyDeleted == true)
                    return this.Ok("Company successfully deleted");
                else
                    return this.NotFound("Company doesn't exist for the company id supplied");
            }
            catch (Exception ex)
            {
                _logger.LogError("The following error occured whilst attempting to delete this company: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
