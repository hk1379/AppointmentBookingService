using AppointmentBooking.Context;
using AppointmentBooking.Companies.DTO;
using static AppointmentBooking.Companies.ICompanyService;
using Microsoft.EntityFrameworkCore;
using AppointmentBooking.Context.Models;

namespace AppointmentBooking.Companies
{
    public class CompanyService : ICompanyService
    {
        private readonly ILogger<ICompanyService> _logger;
        private readonly AppointmentBookingDbContext _context;

        public CompanyService(
            ILogger<ICompanyService> logger,
            AppointmentBookingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private async Task<Company?> GetCompanyAsync(int id) =>
            await _context.Company.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);

        private async Task<Company?> GetCompanyTrackingAsync(int id) =>
            await _context.Company.SingleOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);

        public async Task<CompanyResponse?> CompanyInfoToDisplayAsync(int id)
        {
            Company? company = await this.GetCompanyAsync(id).ConfigureAwait(false);

            if (company != null)
            {
                return new CompanyResponse()
                {
                    Id = company.Id,
                    Name = company.Name,
                    PhoneNumber = company.PhoneNumber,
                    EmailAddress = company.EmailAddress,
                };
            }

            return null;
        }

        public async Task<List<Company>?> GetCompaniesAsync(int[] ids) =>
            await _context.Company.AsNoTracking().Where(c => ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);

        public async Task<List<Company>?> GetAllCompaniesAsync() =>
            await _context.Company.AsNoTracking().ToListAsync();

        public async Task<bool> CreateCompanyAsync(CreateCompanyRequest request)
        {
            int entriesSaved;

            if (request != null)
            {
                Company company = new()
                {
                    Name = request.Name,
                    EmailAddress = request.EmailAddress,
                    PhoneNumber = request.PhoneNumber,
                };

                _context.Company.Add(company);
                entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                _logger.LogError("One of the following are null, Name: {name}, Email Address: {email} or Phone Number: {phoneNumber}",
                                 request?.Name,
                                 request?.EmailAddress,
                                 request?.PhoneNumber);

                throw new ArgumentNullException(nameof(request));
            }

            bool isCompanySaved = entriesSaved == 1;
            return isCompanySaved;
        }

        public async Task<bool> UpdateCompanyAsync(UpdateCompanyRequest request)
        {
            Company? company = await this.GetCompanyTrackingAsync(request.Id).ConfigureAwait(false);

            if (company != null)
            {
                company.Id = request.Id;
                company.Name = request.Name;
                company.PhoneNumber = request.PhoneNumber;
                company.EmailAddress = request.EmailAddress;
            }

            int entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);
            return entriesSaved >= 1;
        }

        public async Task<bool> DeleteCompanyAsync(int Id)
        {
            Company? company = await this.GetCompanyAsync(Id).ConfigureAwait(false);

            if (company != null)
            {
                _context.Remove(company);
                int entriesSaved = await _context.SaveChangesAsync().ConfigureAwait(false);

                return entriesSaved >= 1;
            }

            return false;
        }
    }
}
