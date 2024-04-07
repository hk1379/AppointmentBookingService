namespace AppointmentBooking.Meetings
{
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Meetings.DTO;

    public interface IMeetingService
    {
        public record CreateMeetingRequest(string Title, DateTime FromDateTime, int Duration, string Status, string Location, string Description, bool ReservationPaid);

        public record UpdateMeetingRequest(int Id, string Title, DateTime FromDateTime, int Duration, string Status, string Location, string Description, bool ReservationPaid);

        public record CustomersToMeetingRequest(int MeetingId, int[] CustomerIds);

        public record CompaniesToMeetingRequest(int MeetingId, int[] CompanyIds);

        Task<Meeting?> GetMeetingAsync(int Id);

        Task<MeetingResponse?> MeetingInfoToDisplayAsync(int Id);

        Task<List<Meeting>?> GetMeetingsAsync(int[] ids);

        Task<List<Meeting>?> GetAllMeetingsAsync();

        Task<bool> CreateMeetingAsync(CreateMeetingRequest request);

        Task<bool> UpdateMeetingAsync(UpdateMeetingRequest request);

        Task<bool> AddCustomersToMeeting(CustomersToMeetingRequest request);

        Task<bool> AddCompaniesToMeeting(CompaniesToMeetingRequest request);

        Task<bool> RemoveCustomersFromMeeting(CustomersToMeetingRequest request);

        Task<bool> RemoveCompaniesFromMeeting(CompaniesToMeetingRequest request);

        Task<bool> DeleteMeetingAsync(int Id);
    }
}
