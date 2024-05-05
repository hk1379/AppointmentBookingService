namespace AppointmentBooking.Meetings
{
    using System;
    using AppointmentBooking.Companies;
    using AppointmentBooking.Context;
    using AppointmentBooking.Context.Models;
    using AppointmentBooking.Customers;
    using AppointmentBooking.Meetings.DTO;
    using Microsoft.EntityFrameworkCore;
    using static AppointmentBooking.Meetings.IMeetingService;

    public class MeetingService : IMeetingService
    {
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        private readonly ILogger<MeetingService> _logger;
        private readonly AppointmentBookingDbContext _context;

        public MeetingService(
            ICustomerService customerService,
            ICompanyService companyService,
            ILogger<MeetingService> logger,
            AppointmentBookingDbContext context)
        {
            _customerService = customerService;
            _companyService = companyService;
            _logger = logger;
            _context = context;
        }

        private async Task<Meeting?> GetMeetingAsync(int Id) =>
            await _context.Meetings.AsNoTracking().SingleOrDefaultAsync(m => m.Id == Id).ConfigureAwait(false);

        private async Task<Meeting?> GetMeetingTrackingAsync(int Id) =>
            await _context.Meetings.SingleOrDefaultAsync(m => m.Id == Id).ConfigureAwait(false);

        public async Task<MeetingResponse?> MeetingInfoToDisplayAsync(int Id)
        {
            Meeting? meeting = await this.GetMeetingAsync(Id).ConfigureAwait(false);

            if (meeting != null)
            {
                return new MeetingResponse()
                {
                    Id = Id,
                    FromDateTime = meeting.FromDateTime,
                    Duration = meeting.Duration,
                    Title = meeting.Title,
                    Status = meeting.Status,
                    Location = meeting.Location,
                    Description = meeting.Description,
                    ReservationPaid = meeting.ReservationPaid,
                };
            }

            return null;
        }

        public async Task<List<Meeting>?> GetAllMeetingsAsync() =>
            await _context.Meetings.AsNoTracking().ToListAsync();

        public bool CreateMeeting(CreateMeetingRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.FromDateTime);
            ArgumentNullException.ThrowIfNull(request.Duration);
            ArgumentException.ThrowIfNullOrEmpty(request.Title);
            ArgumentException.ThrowIfNullOrEmpty(request.Status);
            ArgumentException.ThrowIfNullOrEmpty(request.Location);
            ArgumentException.ThrowIfNullOrEmpty(request.Description);

            Meeting meeting = new()
            {
                FromDateTime = request.FromDateTime,
                Duration = request.Duration,
                Title = request.Title,
                Status = request.Status,
                Location = request.Location,
                Description = request.Description,
                ReservationPaid = request.ReservationPaid,
            };

            _context.Meetings.Add(meeting);
            int entriesSaved = _context.SaveChanges();

            bool isMeetingSaved = entriesSaved >= 1;
            return isMeetingSaved;
        }

        public async Task<bool> UpdateMeetingAsync(UpdateMeetingRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Meeting? meeting = await this.GetMeetingTrackingAsync(request.Id).ConfigureAwait(false);

            if (meeting != null)
            {
                meeting.FromDateTime = request.FromDateTime;
                meeting.Duration = request.Duration;
                meeting.Title = request.Title;
                meeting.Status = request.Status;
                meeting.Location = request.Location;
                meeting.Description = request.Description;
                meeting.ReservationPaid = request.ReservationPaid;
            };

            int entriesSaved = _context.SaveChanges();
            return entriesSaved >= 1;
        }

        public async Task<bool> AddCustomersToMeetingAsync(CustomersToMeetingRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            int entriesSaved = 0;

            Task<Meeting?> meeting = this.GetMeetingTrackingAsync(request.MeetingId);
            Task<List<Customer>?> customers = _customerService.GetCustomersAsync(request.CustomerIds);
            await Task.WhenAll(meeting, customers);
            Meeting? meetingResult = await meeting.ConfigureAwait(false);
            List<Customer>? customerResult = await customers.ConfigureAwait(false);

            if (meetingResult != null && customerResult != null)
            {
                meetingResult.Customers = customerResult;
                entriesSaved = _context.SaveChanges();
            }

            return entriesSaved >= 1;
        }

        public async Task<bool> AddCompaniesToMeetingAsync(CompaniesToMeetingRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            int entriesSaved = 0;

            Task<Meeting?> meeting = this.GetMeetingTrackingAsync(request.MeetingId);
            Task<List<Company>?> companies = _companyService.GetCompaniesAsync(request.CompanyIds);
            await Task.WhenAll(meeting, companies);
            Meeting? meetingResult = await meeting.ConfigureAwait(false);
            List<Company>? companyResult = await companies.ConfigureAwait(false);

            if (meetingResult != null && companyResult != null)
            {
                meetingResult.Companies = companyResult;
                entriesSaved = _context.SaveChanges();
            }

            return entriesSaved >= 1;
        }

        public async Task<bool> RemoveCustomersFromMeetingAsync(CustomersToMeetingRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Meeting? meeting = await _context
                .Meetings
                .Include(m => m.Customers)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId)
                .ConfigureAwait(false);

            meeting?.Customers?.RemoveAll(c => request.CustomerIds.Contains(c.Id));

            int entriesSaved = _context.SaveChanges();
            return entriesSaved >= 1;
        }

        public async Task<bool> RemoveCompaniesFromMeetingAsync(CompaniesToMeetingRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Meeting? meeting = await _context
                .Meetings
                .Include(m => m.Companies)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId)
                .ConfigureAwait(false);

            meeting?.Companies?.RemoveAll(c => request.CompanyIds.Contains(c.Id));

            int entriesSaved = _context.SaveChanges();
            return entriesSaved >= 1;
        }

        public async Task<bool> DeleteMeetingAsync(int id)
        {
            Meeting? meeting = await this.GetMeetingAsync(id).ConfigureAwait(false);

            if (meeting != null)
            {
                _context.Remove(meeting);
                int entriesSaved = _context.SaveChanges();

                return entriesSaved >= 1;
            }
            
            return false;
        }
    }
}
