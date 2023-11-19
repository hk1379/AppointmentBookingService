namespace AppointmentBooking.Meetings
{
    using AppointmentBooking.Context;
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Customers.DTO;
    using Microsoft.EntityFrameworkCore;
    using static AppointmentBooking.Meetings.IMeetingService;

    public class MeetingService : IMeetingService
    {
        private readonly ILogger<MeetingService> _logger;
        private readonly AppointmentBookingDbContext _context;

        public MeetingService(ILogger<MeetingService> logger, AppointmentBookingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private async Task<Meeting?> GetMeetingAsync(int Id) =>
            await _context.Meetings.AsNoTracking().SingleOrDefaultAsync(m => m.Id == Id).ConfigureAwait(false); 

        public async Task<MeetingResponse?> MeetingInfoToDisplayAsync(int Id)
        {
            Meeting? meeting = await this.GetMeetingAsync(Id);

            if (meeting != null)
            {
                return new MeetingResponse()
                {
                    Id = meeting.Id,
                    MeetingName = meeting.MeetingName,
                    CustomerPhoneNumber = meeting.CustomerPhoneNumber,
                    CompanyPhoneNumber = meeting.CompanyPhoneNumber,
                    CustomerEmailAddress = meeting.CustomerEmailAddress,
                    CompanyEmailAddress = meeting.CompanyEmailAddress,
                };
            }

            return null;
        }

        public async Task<IEnumerable<Meeting?>> GetMeetingsAsync() =>
            await _context.Meetings.AsNoTracking().ToListAsync().ConfigureAwait(false);

        public async Task<bool> CreateMeetingAsync(CreateMeetingRequest request)
        {
            int entriesSaved;

            if (request != null)
            {
                Meeting meeting = new()
                {
                    MeetingName = request.MeetingName,
                    CustomerPhoneNumber = request.CustomerPhoneNumber,
                    CompanyPhoneNumber = request.CompanyPhoneNumber,
                    CustomerEmailAddress = request.CustomerEmailAddress,
                    CompanyEmailAddress = request.CompanyEmailAddress,
                };

                _context.Meetings.Add(meeting);
                entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                _logger.LogError("MeetingName is null: {name}", request?.MeetingName);

                throw new ArgumentNullException(nameof(request));
            }

            bool isMeetingSaved = entriesSaved >= 1;
            return isMeetingSaved;
        }

        public async Task<bool> UpdateMeetingAsync(UpdateMeetingRequest request)
        {
            Meeting? meeting = await this.GetMeetingAsync(request.Id);

            if (meeting != null)
            {
                meeting.MeetingName = request.MeetingName;
                meeting.CustomerPhoneNumber = request.CustomerPhoneNumber;
                meeting.CompanyPhoneNumber = request.CompanyPhoneNumber;
                meeting.CustomerEmailAddress = request.CustomerEmailAddress;
                meeting.CompanyEmailAddress = request.CompanyEmailAddress;

                _context.Update(meeting);
            };

            int entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            return entriesSaved >= 1;
        }

        public async Task<bool> DeleteMeetingAsync(int id)
        {
            Meeting? meeting = await this.GetMeetingAsync(id).ConfigureAwait(false);

            if (meeting != null)
            {
                _context.Remove(meeting);
                int entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);

                return entriesSaved >= 1;
            }
            
            return false;
        }
    }
}
