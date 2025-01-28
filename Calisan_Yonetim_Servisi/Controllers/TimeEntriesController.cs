using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Constant;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Servisi.Controllers
{
    [Authorize]
    [Route("api/v1/company/{companyId}/time-entries")]
    [ApiController]
    public class TimeEntriesController : ControllerBase
    {
        private readonly ITimeEntriesService _timeEntriesService;

        public TimeEntriesController(ITimeEntriesService timeEntriesService)
        {
            _timeEntriesService = timeEntriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeEntriesByCompany(Guid companyId,
            [FromQuery] Guid personnelId,
            [FromQuery] Guid projectId,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var result = await _timeEntriesService.GetTimeEntries(companyId, personnelId, projectId,startDate, endDate);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> BulkAdd([FromBody] BulkAddTimeEntriesRequest request)
        {
            await _timeEntriesService.BulkAddAsync(request.TimeEntries);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TimeEntriesDto timeEntry)
        {
            await _timeEntriesService.UpdateAsync(id, timeEntry);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _timeEntriesService.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("bulk")]
        public async Task<IActionResult> BulkDelete([FromBody] List<Guid> ids)
        {
            await _timeEntriesService.BulkDeleteAsync(ids);
            return NoContent();
        }
    }
} 