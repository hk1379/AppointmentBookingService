using System.ComponentModel.DataAnnotations;

namespace AppointmentBooking.Meetings.Requests
{
    public class CreateMeetingRequest
    {
        [Required]
        public DateTime FromDateTime { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Status {  get; set; } // make enum for the different statuses

        [Required]
        public required string Location { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required bool? ReservationPaid { get; set; }

        // add validator method
        // add rule about needing atleast one of to email address or to phone number
    }
}
