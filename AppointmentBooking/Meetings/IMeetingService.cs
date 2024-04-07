namespace AppointmentBooking.Meetings
{
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Customers.DTO;
    using AppointmentBooking.Meetings.Requests;

    public interface IMeetingService
    {
        // public record CreateMeetingRequest(string MeetingName, string? CustomerPhoneNumber, string? CompanyPhoneNumber, string? CustomerEmailAddress, string? CompanyEmailAddress, int[] CustomerIds);

        // public record UpdateMeetingRequest(int Id, string MeetingName, string? CustomerPhoneNumber, string? CompanyPhoneNumber, string? CustomerEmailAddress, string? CompanyEmailAddress, int[] CustomerIds);

        public record CustomersToMeetingRequest(int MeetingId, int[] CustomerIds);

        Task<Meeting?> GetMeetingAsync(int Id);

        Task<MeetingResponse?> MeetingInfoToDisplayAsync(int Id);

        Task<List<Meeting>?> GetMeetingsAsync(int[] ids);

        Task<List<Meeting>?> GetAllMeetingsAsync();

        Task<bool> CreateMeetingAsync(CreateMeetingRequest request);

        Task<bool> UpdateMeetingAsync(UpdateMeetingRequest request);

        Task<bool> AddCustomersToMeeting(CustomersToMeetingRequest request);

        Task<bool> RemoveCustomersFromMeeting(CustomersToMeetingRequest request);

        Task<bool> DeleteMeetingAsync(int Id);
    }
}
