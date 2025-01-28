using Calisan_Yonetim_Core.Exceptions;
using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Constant;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository _personnelRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenService _tokenService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        public PersonnelService(IPersonnelRepository personnelRepository, 
            IUserRoleRepository userRoleRepository, 
            IRoleRepository roleRepository, 
            ITokenService tokenService,
            ICompanyRepository companyRepository,
            IUserRepository userRepository
            )
        {
            _personnelRepository = personnelRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _tokenService = tokenService;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<PersonnelListDto>> GetAllPersonnelAsync(Guid companyId)
        {
            var role = _tokenService.GetUserRoleFromToken();
            var currentUserId = _tokenService.GetUserUserIdFromToken();

            return await _personnelRepository.GetAll()
                                             .Include(p => p.User)
                                             .ThenInclude(u => u.Company)
                                             .Join(_userRoleRepository.GetAll(),
                                                    p => p.User.Id,   // User tablosundaki Id ile
                                                    ur => ur.UserId,  // UserRole tablosundaki UserId ile
                                                    (p, ur) => new { p, ur })  // Kullanıcı ve UserRole tablosunu birleştir
                                                  .Join(_roleRepository.GetAll(),
                                                    ur => ur.ur.RoleId,  // UserRole tablosundaki RoleId ile
                                                    r => r.RoleId,  // Role tablosundaki RoleId ile
                                                    (ur, r) => new { ur.p, ur, r })  // UserRole ve Role tablosunu birleştir
                                                  .Where(x => (x.p.User.CompanyId == companyId || role == UserRoleConstant.SystemAdmin) && x.ur.ur.Status == Status.Active
                                                                   && (x.p.User.Id == Guid.Parse(currentUserId) || role != UserRoleConstant.User))
                                                  .Select(x => new PersonnelListDto
                                                  {
                                                      PersonnelId = x.p.PersonnelId,
                                                      FirstName = x.p.FistName,
                                                      LastName = x.p.LastName,
                                                      UserName = x.p.User.Username,
                                                      Email = x.p.Email,
                                                      UserCompanyId = x.p.User.CompanyId,
                                                      RoleId = x.r.RoleId,
                                                      CompanyName = x.p.User.Company.CompanyName,
                                                      RoleName = x.r.RoleName,
                                                  }).ToListAsync();
        }

        public async Task CreatePersonnelAsync(Guid companyId, PersonnelUserDto personnelUserDto)
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

            // Check if role exists
            var role = await _roleRepository.GetAll()
                .FirstOrDefaultAsync(r=>r.RoleId == personnelUserDto.Role.RoleId);

            if (role == null)
            {
                throw new ServiceHttpException(HttpStatusCode.BadRequest, "Role doesn't exist");
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



            var newUserRole = new UserRole
            {
                RoleId = personnelUserDto.Role.RoleId,
                UserId = newUser.Id,
                Status = Status.Active
            };

            await _userRoleRepository.Insert(newUserRole);
        }

        public async Task UpdatePersonnelAsync(Guid companyId, Guid employeeId, PersonnelUserDto personnelUserDto)
        {
            // Check if company exists
            var companyExists = await _companyRepository.GetAll()
                .AnyAsync(c => c.CompanyId == personnelUserDto.User.CompanyId && c.CompanyStatus == Status.Active);

            if (!companyExists)
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, "Company not found or inactive");
            }

            // Get existing personnel
            var existingPersonnel = await _personnelRepository.GetAll()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PersonnelId == employeeId);

            if (existingPersonnel == null)
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, "Personnel not found");
            }

            // Check if username exists for other users
            var existingUser = await _userRepository.GetAll()
                .FirstOrDefaultAsync(u => u.Username == personnelUserDto.User.Username && u.Id != existingPersonnel.User.Id);

            if (existingUser != null)
            {
                throw new ServiceHttpException(HttpStatusCode.BadRequest, "Username already exists");
            }

            // Check if role exists
            var role = await _roleRepository.GetAll()
                .FirstOrDefaultAsync(r => r.RoleId == personnelUserDto.Role.RoleId);

            if (role == null)
            {
                throw new ServiceHttpException(HttpStatusCode.BadRequest, "Role doesn't exist");
            }

            // Update personnel information
            existingPersonnel.FistName = personnelUserDto.Personnel.FirstName;
            existingPersonnel.LastName = personnelUserDto.Personnel.LastName;
            existingPersonnel.Email = personnelUserDto.Personnel.Email;

            // Update user information
            existingPersonnel.User.Username = personnelUserDto.User.Username;
            if (!string.IsNullOrEmpty(personnelUserDto.User.Password))
            {
                existingPersonnel.User.Password = personnelUserDto.User.Password;
            }
            existingPersonnel.User.CompanyId = personnelUserDto.User.CompanyId;

            // Update user role
            var existingUserRole = await _userRoleRepository.GetAll()
                .FirstOrDefaultAsync(ur => ur.UserId == existingPersonnel.User.Id && ur.Status == Status.Active);

            if (existingUserRole != null)
            {
                if(existingUserRole.RoleId != personnelUserDto.Role.RoleId)
                {
                    await _userRoleRepository.Delete(existingUserRole);
                    var newUserRole = new UserRole
                    {
                        RoleId = personnelUserDto.Role.RoleId,
                        UserId = existingPersonnel.User.Id,
                        Status = Status.Active
                    };
                    await _userRoleRepository.Insert(newUserRole);
                }
            }

            await _personnelRepository.Update(existingPersonnel);
            await _userRepository.Update(existingPersonnel.User);
        }

        public async Task DeletePersonnelAsync(Guid companyId, Guid employeeId)
        {
            var currentUserRole = _tokenService.GetUserRoleFromToken();

            // Get existing personnel with user information
            var existingPersonnel = await _personnelRepository.GetAll()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PersonnelId == employeeId);

            if (existingPersonnel == null)
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, "Personnel not found");
            }

            // Check if personnel belongs to the company
            if (existingPersonnel.User.CompanyId != companyId && currentUserRole != UserRoleConstant.SystemAdmin)
            {
                throw new ServiceHttpException(HttpStatusCode.BadRequest, "Personnel does not belong to the specified company");
            }

            // Get active user role
            var existingUserRole = await _userRoleRepository.GetAll()
                .FirstOrDefaultAsync(ur => ur.UserId == existingPersonnel.User.Id && ur.Status == Status.Active);

            // Soft delete user role if exists
            if (existingUserRole != null)
            {
                await _userRoleRepository.Delete(existingUserRole);
            }

            // Soft delete user
            await _userRepository.Delete(existingPersonnel.User);

            // Soft delete personnel
            await _personnelRepository.Delete(existingPersonnel);
        }
    }
} 