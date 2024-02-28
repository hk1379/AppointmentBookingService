namespace AppointmentBooking.Meetings
{
    using System.Net;
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Customers.DTO;
    using static AppointmentBooking.Meetings.IMeetingService;
    using Microsoft.AspNetCore.Mvc;

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
                IEnumerable<Meeting?>? meetings = await _meetingService.GetMeetingsAsync().ConfigureAwait(false);

                if (meetings != null)
                {
                    return this.Ok(meetings);
                }
                else
                {
                    return this.NotFound();
                }
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
                
                return this.StatusCode((int) HttpStatusCode.BadRequest);
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
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateMeetingRequest request)
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

                return this.StatusCode((int) HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error whilst updating customer: {error}", ex.Message);
                return this.StatusCode((int) HttpStatusCode.InternalServerError);
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