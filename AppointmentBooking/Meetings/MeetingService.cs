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

        public async Task<Meeting?> GetMeetingAsync(int Id) =>
            await _context.Meetings.AsNoTracking().SingleOrDefaultAsync(m => m.Id == Id).ConfigureAwait(false);

        private async Task<Meeting?> GetMeetingTrackingAsync(int Id) =>
            await _context.Meetings.SingleOrDefaultAsync(m => m.Id == Id).ConfigureAwait(false);

        public async Task<MeetingResponse?> MeetingInfoToDisplayAsync(int Id)
        {
            Meeting? meeting = await this.GetMeetingAsync(Id);

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

        public async Task<List<Meeting>?> GetMeetingsAsync(int[] ids) =>
            await _context.Meetings.AsNoTracking().Where(m => ids.Contains(m.Id)).ToListAsync();

        public async Task<List<Meeting>?> GetAllMeetingsAsync() =>
            await _context.Meetings.AsNoTracking().ToListAsync();

        public async Task<bool> CreateMeetingAsync(CreateMeetingRequest request)
        {
            int entriesSaved;
            // List<Customer>? customers = await _customerService.GetCustomersAsync(request.CustomerIds).ConfigureAwait(false);

            if (request != null)
            {
                Meeting meeting = new()
                {
                    // CompanyId = request.CompanyId,
                    FromDateTime = request.FromDateTime,
                    Duration = request.Duration,
                    Title = request.Title,
                    Status = request.Status,
                    Location = request.Location,
                    Description = request.Description,
                    ReservationPaid = request.ReservationPaid,
                };

                _context.Meetings.Add(meeting);
                entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);

                //if (!customers.IsNullOrEmpty())
                //{
                //    meeting.Customers = customers;

                //    _context.Meetings.Update(meeting);
                //    entriesSaved += await _context.SaveChangesAsync().ConfigureAwait(false);
                //}
            }
            else
            {
                _logger.LogError("Create Meeting Request is null: {request}", request);
                throw new ArgumentNullException(nameof(request));
            };

            bool isMeetingSaved = entriesSaved >= 1;
            return isMeetingSaved;
        }

        public async Task<bool> UpdateMeetingAsync(UpdateMeetingRequest request)
        {
            Meeting? meeting = await this.GetMeetingTrackingAsync(request.Id).ConfigureAwait(false);
            // List<Customer>? customers = await _customerService.GetCustomersAsync(request.CustomerIds).ConfigureAwait(false);

            if (meeting != null)
            {
                meeting.FromDateTime = request.FromDateTime;
                meeting.Duration = request.Duration;
                meeting.Title = request.Title;
                meeting.Status = request.Status;
                meeting.Location = request.Location;
                meeting.Description = request.Description;
                meeting.ReservationPaid = request.ReservationPaid;

            //    if (customers != null)
            //        meeting.Customers = customers;
            //
            //    _context.Meetings.Update(meeting);
            };

            int entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            return entriesSaved >= 1;
        }

        public async Task<bool> AddCustomersToMeeting(CustomersToMeetingRequest request)
        {
            int entriesSaved = 0;

            Meeting? meeting = await this.GetMeetingTrackingAsync(request.MeetingId).ConfigureAwait(false);
            List<Customer>? customers = await _customerService.GetCustomersAsync(request.CustomerIds).ConfigureAwait(false);

            if (meeting != null && customers != null)
            {
                meeting.Customers = customers;
                entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            }

            return entriesSaved >= 1;
        }

        public async Task<bool> AddCompaniesToMeeting(CompaniesToMeetingRequest request)
        {
            int entriesSaved = 0;

            Meeting? meeting = await this.GetMeetingTrackingAsync(request.MeetingId).ConfigureAwait(false);
            List<Company>? companies = await _companyService.GetCompaniesAsync(request.CompanyIds).ConfigureAwait(false);

            if (meeting != null && companies != null)
            {
                meeting.Companies = companies;
                entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            }

            return entriesSaved >= 1;
        }

        public async Task<bool> RemoveCustomersFromMeeting(CustomersToMeetingRequest request)
        {
            Meeting? meeting = await _context
                .Meetings
                .Include(m => m.Customers)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId)
                .ConfigureAwait(false);

            meeting?.Customers?.RemoveAll(c => request.CustomerIds.Contains(c.Id));

            int entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            return entriesSaved >= 1;
        }

        public async Task<bool> RemoveCompaniesFromMeeting(CompaniesToMeetingRequest request)
        {
            Meeting? meeting = await _context
                .Meetings
                .Include(m => m.Companies)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId)
                .ConfigureAwait(false);

            meeting?.Companies?.RemoveAll(c => request.CompanyIds.Contains(c.Id));

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
