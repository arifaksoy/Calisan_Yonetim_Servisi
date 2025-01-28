using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Constant;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Servisi.Controllers
{

    [Authorize]
    [Route("api/v1/company/{companyId}/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectsByCompany(Guid companyId)
        {
            var result = await _projectService.GetProjects(companyId);
            return Ok(result);
        }
        [Authorize(Policy = PolicyConstant.AdminOrSystemAdmin)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProjectDto project)
        {
            await _projectService.AddAsync(project);
            return Ok();
        }
        [Authorize(Policy = PolicyConstant.AdminOrSystemAdmin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProjectDto project)
        {
             await _projectService.UpdateAsync(id,project);
            return Ok();
        }
        [Authorize(Policy = PolicyConstant.AdminOrSystemAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _projectService.DeleteAsync(id);
            return NoContent();
        }
    }
} 