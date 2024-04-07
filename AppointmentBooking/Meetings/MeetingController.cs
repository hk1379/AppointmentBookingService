namespace AppointmentBooking.Meetings
{
    using System.Net;
    using AppointmentBooking.Context.Models;
    using static AppointmentBooking.Meetings.IMeetingService;
    using Microsoft.AspNetCore.Mvc;
    using AppointmentBooking.Meetings.Requests;

    [Route("api/[controller]")]
    public class MeetingController : Controller
    {
        private readonly ILogger<MeetingController> _logger;

        private readonly IMeetingService _meetingService;

        public MeetingController(ILogger<MeetingController> logger, IMeetingService meetingService)
        {
            _logger = logger;
            _meetingService = meetingService;
        }

        // working
        [HttpGet]
        [Route("{meetingId}")]
        public async Task<IActionResult> GetMeetingAsync([FromRoute] int meetingId)
        {
            try
            {
                MeetingResponse? meeting = await _meetingService.MeetingInfoToDisplayAsync(meetingId).ConfigureAwait(false);

                if (meeting != null)
                {
                    return this.Ok(meeting);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while searching for meeting: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // working
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetMeetingsAsync()
        {
            try
            {
                IEnumerable<Meeting?>? meetings = await _meetingService.GetAllMeetingsAsync().ConfigureAwait(false);

                if (meetings != null)
                    return this.Ok(meetings);
                else
                    return this.NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while searching for all meetings: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // working
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateMeetingAsync([FromBody] CreateMeetingRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool meetingCreated = await _meetingService.CreateMeetingAsync(request);

                    if (meetingCreated == true)
                        return this.Ok("Meeting Created");
                    else
                        return this.StatusCode((int) HttpStatusCode.InternalServerError);
                }
                
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst creating meeting: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        // working
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateMeetingAsync([FromBody] UpdateMeetingRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool meetingUpdated = await _meetingService.UpdateMeetingAsync(request).ConfigureAwait(false);

                    if (meetingUpdated == true)
                        return this.Ok("Meeting Updated");
                    else
                        return this.StatusCode((int) HttpStatusCode.InternalServerError);
                }

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst updating meeting: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("addCustomers")]
        public async Task<IActionResult> AddCustomersToMeetingAsync([FromBody] CustomersToMeetingRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool customerAdded = await _meetingService.AddCustomersToMeeting(request).ConfigureAwait(false);

                    if (customerAdded == true)
                        return this.Ok("Customers Added");
                    else
                        return this.StatusCode((int) HttpStatusCode.InternalServerError);
                }

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst adding customer to meeting: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("addCompanies")]
        public async Task<IActionResult> AddCompaniesToMeetingAsync([FromBody] CompaniesToMeetingRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool companyAdded = await _meetingService.AddCompaniesToMeeting(request).ConfigureAwait(false);

                    if (companyAdded == true)
                        return this.Ok("Companies Added");
                    else
                        return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst adding company to meeting: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("removeCustomers")]
        public async Task<IActionResult> RemoveCustomersFromMeetingAsync([FromBody] CustomersToMeetingRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool customersRemoved = await _meetingService.RemoveCustomersFromMeeting(request).ConfigureAwait(false);

                    if (customersRemoved == true)
                        return this.Ok("Customers Removed");
                    else
                        return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst removing customers from meeting: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("removeCompanies")]
        public async Task<IActionResult> RemoveCompaniesFromMeetingAsync([FromBody] CompaniesToMeetingRequest request)
        {
            try
            {
                if (request != null)
                {
                    bool companiesRemoved = await _meetingService.RemoveCompaniesFromMeeting(request).ConfigureAwait(false);

                    if (companiesRemoved == true)
                        return this.Ok("Companies Removed");
                    else
                        return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst removing companies from meeting: {error}", ex.Message);
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // working
        [HttpDelete]
        [Route("{meetingId}")]
        public async Task<IActionResult> DeleteCustomerAsync([FromRoute] int meetingId)
        {
            try
            {
                bool meetingDeleted = await _meetingService.DeleteMeetingAsync(meetingId).ConfigureAwait(false);

                if (meetingDeleted == true)
                    return this.Ok("Meeting successfully deleted");
                else
                    return this.NotFound("Meeting doesn't exist for the meetong id supplied");
            }
            catch (Exception ex)
            {
                _logger.LogError("The following error occured whilst attempting to delete this meeting: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}