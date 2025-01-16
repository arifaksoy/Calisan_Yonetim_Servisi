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
    public class AccountService : IAccountServices
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPersonnelRepository _personnelRepository;

        public AccountService(IPersonnelRepository personnelRepository, ITokenService tokenService, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _personnelRepository = personnelRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }
        public async Task<string> Login(UserDto user)
        {
            var userFromDb = await _userRepository.GetAll()
                                                  .Where(u => u.Username == user.Username && u.Password == user.Password)
                                                  .Include(p => p.Personnel)
                                                  .Include(c => c.Company)
                                                  .Join(_userRoleRepository.GetAll(),
                                                    u => u.Id,   // User tablosundaki Id ile
                                                    ur => ur.UserId,  // UserRole tablosundaki UserId ile
                                                    (u, ur) => new { u, ur })  // Kullanıcı ve UserRole tablosunu birleştir
                                                  .Join(_roleRepository.GetAll(),
                                                    ur => ur.ur.RoleId,  // UserRole tablosundaki RoleId ile
                                                    r => r.RoleId,  // Role tablosundaki RoleId ile
                                                    (ur, r) => new { ur.u, ur, r })  // UserRole ve Role tablosunu birleştir
                                                  .Select(x => new TokenClaimDto
                                                  {
                                                    UserName = x.u.Username,
                                                    RoleName = x.r.RoleName,  // Role tablosundan RoleName
                                                    CompanyId = x.u.CompanyId,
                                                    PersonnelId = x.u.PersonnelId,
                                                    UserId = x.u.Id,
                                                    CompanyName = x.u.Company.CompanyName,
                                                    FullName = x.u.Personnel.FistName + " " + x.u.Personnel.LastName
                                                  })
                                                .FirstOrDefaultAsync();

            if (userFromDb == null)
            {
                throw new ServiceHttpException(HttpStatusCode.InternalServerError, ErrorMessageKeys.ErrorNotFoundUser);
            }

            var token = _tokenService.GenerateToken(userFromDb);
            return token;
        }

    }
}
