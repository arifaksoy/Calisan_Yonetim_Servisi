using EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetRolesByCompanyId(Guid companyId);
    }
} 