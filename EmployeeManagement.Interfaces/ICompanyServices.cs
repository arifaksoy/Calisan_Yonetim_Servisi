using EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface ICompanyServices
    {
        Task AddNewCompany(CompanyDto company);
        Task UpdateCompany(Guid companyId, CompanyDto company);
        Task DeleteCompany(Guid companyId);
        Task<List<CompanyDto>> GetActiveCompanies();
    }
}
