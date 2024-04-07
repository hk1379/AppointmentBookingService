using System.ComponentModel.DataAnnotations;

namespace AppointmentBooking.Meetings.Requests
{
    public class UpdateMeetingRequest : CreateMeetingRequest
    {
        [Required]
        public required int Id { get; set; }

        // add validator method
    }
}
