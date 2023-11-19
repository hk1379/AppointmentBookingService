namespace AppointmentBooking.Meetings
{
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Customers.DTO;

    public interface IMeetingService
    {
        public record CreateMeetingRequest(string MeetingName, string? CustomerPhoneNumber, string? CompanyPhoneNumber, string? CustomerEmailAddress, string? CompanyEmailAddress);

        public record UpdateMeetingRequest(int Id, string MeetingName, string? CustomerPhoneNumber, string? CompanyPhoneNumber, string? CustomerEmailAddress, string? CompanyEmailAddress);

        public Task<MeetingResponse?> MeetingInfoToDisplayAsync(int Id);

        public Task<IEnumerable<Meeting?>> GetMeetingsAsync();

        public Task<bool> CreateMeetingAsync(CreateMeetingRequest request);

        public Task<bool> UpdateMeetingAsync(UpdateMeetingRequest request);

        public Task<bool> DeleteMeetingAsync(int Id);
    }
}
