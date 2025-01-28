using Calisan_Yonetim_Core.Exceptions;
using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Constant;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EmployeeManagement.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ITokenService _tokenService;

        public RoleService(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, ITokenService tokenService)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _tokenService = tokenService;
        }

        public async Task<List<RoleDto>> GetRolesByCompanyId(Guid companyId)
        {
            try
            {
                var userRole = _tokenService.GetUserRoleFromToken();

                var roles = await _roleRepository.GetAll()
                    .Where(r => r.Status == Status.Active && (r.RoleName != UserRoleConstant.SystemAdmin || userRole == UserRoleConstant.SystemAdmin))
                    .Select(r => new RoleDto
                        {
                            RoleId = r.RoleId,
                            RoleName = r.RoleName
                        })
                    .Distinct()
                    .ToListAsync();

                return roles;
            }
            catch (Exception ex)
            {
                throw new ServiceHttpException(HttpStatusCode.InternalServerError, $"Error retrieving roles: {ex.Message}");
            }
        }
    }
} 