using EmployeeManagement.Common.Constant;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calisan_Yonetim_Servisi.Controllers
{
    [Route("api/v1/company/{companyId}/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize(Policy = PolicyConstant.AdminOrSystemAdmin)]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRolesByCompanyId(Guid companyId)
        {
            try
            {
                var roles = await _roleService.GetRolesByCompanyId(companyId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 