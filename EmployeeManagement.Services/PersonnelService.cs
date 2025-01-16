using EmployeeManagement.Common.Constant;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository _personnelRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenService _tokenService;
        public PersonnelService(IPersonnelRepository personnelRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ITokenService tokenService)
        {
            _personnelRepository = personnelRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<PersonnelListDto>> GetAllPersonnelAsync(Guid companyId)
        {
            var role = _tokenService.GetUserRoleFromToken();

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
                                                  .Where(x => x.p.User.CompanyId == companyId || role == UserRoleConstant.SystemAdmin)
                                                  .Select(x => new PersonnelListDto
                                                  {
                                                      PersonnelId = x.p.PersonnelId,
                                                      FirstName = x.p.FistName,
                                                      LastName = x.p.FistName,
                                                      CompanyName = x.p.User.Company.CompanyName,
                                                      RoleName = x.r.RoleName,
                                                  }).ToListAsync();
        }
    }
} 