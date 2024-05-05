using AppointmentBooking.Companies.DTO;
using AppointmentBooking.Context.Models;

namespace AppointmentBooking.Companies
{
    public interface ICompanyService
    {
        record CreateCompanyRequest(string Name, string PhoneNumber, string EmailAddress);

        record UpdateCompanyRequest(int Id, string Name, string PhoneNumber, string EmailAddress);

        Task<CompanyResponse?> CompanyInfoToDisplayAsync(int companyId);

        Task<List<Company>?> GetCompaniesAsync(int[] ids);

        Task<List<Company>?> GetAllCompaniesAsync();

        bool CreateCompany(CreateCompanyRequest request);

        Task<bool> UpdateCompanyAsync(UpdateCompanyRequest request);

        Task<bool> DeleteCompanyAsync(int companyId);
    }
}
