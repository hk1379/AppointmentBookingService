namespace AppointmentBooking.Meetings
{
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Meetings.DTO;

    public interface IMeetingService
    {
        record CreateMeetingRequest(string Title, DateTime FromDateTime, int Duration, string Status, string Location, string Description, bool ReservationPaid);

        record UpdateMeetingRequest(int Id, string Title, DateTime FromDateTime, int Duration, string Status, string Location, string Description, bool ReservationPaid);

        record CustomersToMeetingRequest(int MeetingId, int[] CustomerIds);

        record CompaniesToMeetingRequest(int MeetingId, int[] CompanyIds);

        Task<MeetingResponse?> MeetingInfoToDisplayAsync(int Id);

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
