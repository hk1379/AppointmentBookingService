using AppointmentBooking.Companies.DTO;
using AppointmentBooking.Context.Models;

namespace AppointmentBooking.Companies
{
    public interface ICompanyService
    {
        public record CreateCompanyRequest(string Name, string PhoneNumber, string EmailAddress, int[] MeetingIds);

        public record UpdateCompanyRequest(int Id, string Name, string PhoneNumber, string EmailAddress, int[] MeetingIds);

        Task<Company?> GetCompanyAsync(int Id);

        Task<CompanyResponse?> CompanyInfoToDisplayAsync(int companyId);

        Task<List<Company>?> GetCompaniesAsync(int[] ids);

        Task<List<Company>?> GetAllCompaniesAsync();

        Task<bool> CreateCompanyAsync(CreateCompanyRequest request);

        Task<bool> UpdateCompanyAsync(UpdateCompanyRequest request);

        Task<bool> DeleteCompanyAsync(int companyId);
    }
}
