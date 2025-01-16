using Calisan_Yonetim_Core;
using Calisan_Yonetim_Core.Exceptions;
using Calisan_Yonetim_Core.Models;
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
        public CompanyService(ICompanyRepository companyRepository, IPersonnelRepository personnelRepository, ITokenService tokenService, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _companyRepository = companyRepository;
            _personnelRepository = personnelRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
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

        public async Task AddAdmin(PersonnelUserDto personnelUserDto)
        {
            try
            {
                // Check if company exists
                var companyExists = await _companyRepository.GetAll()
                    .AnyAsync(c => c.CompanyId == personnelUserDto.User.CompanyId && c.CompanyStatus == Status.Active);

                if (!companyExists)
                {
                    throw new ServiceHttpException(HttpStatusCode.NotFound, "Company not found or inactive");
                }

                // Check if username already exists
                var existingUser = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(u => u.Username == personnelUserDto.User.Username);

                if (existingUser != null)
                {
                    throw new ServiceHttpException(HttpStatusCode.BadRequest, "Username already exists");
                }

                var newPersonnel = new Personnel
                {
                    FistName = personnelUserDto.Personnel.FirstName,
                    LastName = personnelUserDto.Personnel.LastName,
                    Email = personnelUserDto.Personnel.Email,
                    Status = Status.Active,
                };

                await _personnelRepository.Insert(newPersonnel);

                var newUser = new User
                {
                    Username = personnelUserDto.User.Username,
                    Password = personnelUserDto.User.Password,
                    Status = Status.Active,
                    PersonnelId = newPersonnel.PersonnelId,
                    CompanyId = personnelUserDto.User.CompanyId,
                };

                await _userRepository.Insert(newUser);

                var adminRole = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.RoleName == "Admin");

                var newUserRole = new UserRole
                {
                    RoleId = adminRole.RoleId,
                    UserId = newUser.Id,
                    Status = Status.Active
                };

                await _userRoleRepository.Insert(newUserRole);
            }
            catch (Exception ex)
            {
                // Consider using a logging service instead of Console.WriteLine
                throw new Exception($"Registration failed: {ex.Message}", ex);
            }
        }

        public async Task<List<CompanyDto>> GetActiveCompanies()
        {
            try
            {
                var activeCompanies = await _companyRepository.GetAll()
                    .Where(c => c.CompanyStatus == Status.Active)
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
