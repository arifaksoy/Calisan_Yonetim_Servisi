using Calisan_Yonetim_Core.Exceptions;
using Calisan_Yonetim_Core.Models;
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

        public RoleService(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<List<RoleDto>> GetRolesByCompanyId(Guid companyId)
        {
            try
            {
                var roles = await _roleRepository.GetAll()
                    .Where(r => r.Status == Status.Active)
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