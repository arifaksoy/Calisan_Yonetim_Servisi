using Calisan_Yonetim_Core;
using Calisan_Yonetim_Core.Exceptions;
using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Constant;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace EmployeeManagement.Services
{
    public class CompanyService : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPersonnelRepository _personnelRepository;
        private readonly ITokenService _tokenService;
        public CompanyService(ICompanyRepository companyRepository, IPersonnelRepository personnelRepository, ITokenService tokenService, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _companyRepository = companyRepository;
            _personnelRepository = personnelRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _tokenService = tokenService;
        }
         
        public async Task  AddNewCompany(CompanyDto company)
        {
            var newCompany = new Company() 
            {
                CompanyId = Guid.NewGuid(),
                CompanyName = company.CompanyName,
                CompanyStatus = Status.Active
            };
           await _companyRepository.Insert(newCompany);
        }

        public async Task<List<CompanyDto>> GetActiveCompanies()
        {
            try
            {
                var userCompanyId =  _tokenService.GetUserCompanyIdFromToken();
                var userRole =  _tokenService.GetUserRoleFromToken();
                

                var activeCompanies = await _companyRepository.GetAll()
                    .Where(c => c.CompanyStatus == Status.Active && (c.CompanyId == Guid.Parse(userCompanyId) || UserRoleConstant.SystemAdmin == userRole ))
                    .Select(c => new CompanyDto
                    {
                        CompanyId = c.CompanyId,
                        CompanyName = c.CompanyName
                    })
                    .ToListAsync();

                return activeCompanies;
            }
            catch (Exception ex)
            {
                throw new ServiceHttpException(HttpStatusCode.InternalServerError, $"Error retrieving active companies: {ex.Message}");
            }
        }

        public async Task UpdateCompany(Guid companyId, CompanyDto companyDto)
        {
            var company = await _companyRepository.GetAll()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.CompanyStatus == Status.Active);

            if (company == null)
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, "Company not found or inactive");
            }

            company.CompanyName = companyDto.CompanyName;
            
            try
            {
                await _companyRepository.Update(company);
            }
            catch (Exception ex)
            {
                throw new ServiceHttpException(HttpStatusCode.InternalServerError, 
                    $"Error updating company: {ex.Message}");
            }
        }

        public async Task DeleteCompany(Guid companyId)
        {
            var company = await _companyRepository.GetAll()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.CompanyStatus == Status.Active);

            if (company == null)
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, "Company not found or inactive");
            }

            // Soft delete - just update the status
            company.CompanyStatus = Status.Inactive;
            
            try
            {
                await _companyRepository.Update(company);
            }
            catch (Exception ex)
            {
                throw new ServiceHttpException(HttpStatusCode.InternalServerError, 
                    $"Error deleting company: {ex.Message}");
            }
        }
    }
}
