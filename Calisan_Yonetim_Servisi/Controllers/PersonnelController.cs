using EmployeeManagement.Common.Constant;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Servisi.Controllers
{
    [Route("api/v1/company/{companyId}/employee")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly IPersonnelService _personnelService;

        public PersonnelController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        [HttpGet]
        [Authorize(Policy = PolicyConstant.SystemAdminOnly)]
        public async Task<ActionResult<IEnumerable<PersonnelListDto>>> GetAll(Guid companyId)
        {
            var result = await _personnelService.GetAllPersonnelAsync(companyId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstant.SystemAdminOnly)]
        public async Task<IActionResult> Create(Guid companyId, [FromBody] PersonnelUserDto personnelUserDto)
        {
            await _personnelService.CreatePersonnelAsync(companyId, personnelUserDto);
            return Ok();
        }

        [HttpPut("{employeeId}")]
        [Authorize(Policy = PolicyConstant.SystemAdminOnly)]
        public async Task<IActionResult> Update(Guid companyId, Guid employeeId, [FromBody] PersonnelUserDto personnelUserDto)
        {
            await _personnelService.UpdatePersonnelAsync(companyId, employeeId, personnelUserDto);
            return Ok();
        }

        [HttpDelete("{employeeId}")]
        [Authorize(Policy = PolicyConstant.SystemAdminOnly)]
        public async Task<IActionResult> Delete(Guid companyId, Guid employeeId)
        {
            await _personnelService.DeletePersonnelAsync(companyId, employeeId);
            return Ok();
        }
    }
} 