using AppointmentBooking.Context.Models;

namespace AppointmentBooking.Customers.Responses
{
    public class MeetingResponse
    {
        public int Id { get; set; }

        public string MeetingName { get; set; } = string.Empty;

        public string? CustomerPhoneNumber { get; set; }

        public string? CompanyPhoneNumber { get; set; }

        public string? CustomerEmailAddress { get; set; }

        public string? CompanyEmailAddress { get; set; }

        public List<string>? Customers { get; set; }
    }
}
