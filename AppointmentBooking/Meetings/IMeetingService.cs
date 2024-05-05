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

        bool CreateMeeting(CreateMeetingRequest request);

        Task<bool> UpdateMeetingAsync(UpdateMeetingRequest request);

        Task<bool> AddCustomersToMeetingAsync(CustomersToMeetingRequest request);

        Task<bool> AddCompaniesToMeetingAsync(CompaniesToMeetingRequest request);

        Task<bool> RemoveCustomersFromMeetingAsync(CustomersToMeetingRequest request);

        Task<bool> RemoveCompaniesFromMeetingAsync(CompaniesToMeetingRequest request);

        Task<bool> DeleteMeetingAsync(int Id);
    }
}
