using Calisan_Yonetim_Core.Models;
using Calisan_Yonetim_Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Common.Constant;
using Calisan_Yonetim_Core.Exceptions;
using EmployeeManagement.Dto;

namespace Calisan_Yonetim_Servisi.Controllers
{
    [ApiController]
    [Route("api/v1/pages/{userId}")]
    [Authorize(Policy = PolicyConstant.User)]
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;
        private readonly CalisanYonetimDbContext _context;

        public PageController(IPageService pageService, CalisanYonetimDbContext context)
        {
            _pageService = pageService;
            _context = context;
        }

        [HttpGet("my-pages")]
        public async Task<ActionResult<IEnumerable<Page>>> GetMyPages(Guid userId)
        {
            var pages =  await _pageService.GetPagesAsync(userId);
            return Ok(pages);
        }

        [HttpPost("add-page")]
        [Authorize(Policy = PolicyConstant.SystemAdminOnly)]
        public async Task<ActionResult<Page>> CreatePage(PageDto page)
        {
            var createdPage = await _pageService.CreatePageAsync(page);
            return Ok("Added page successfully!");
        }

        [HttpPut("page/{id}")]
        [Authorize(Policy = PolicyConstant.SystemAdminOnly)]
        public async Task<IActionResult> UpdatePage(Guid id, [FromBody] PageDto pageDto)
        {
            try
            {
                await _pageService.UpdatePageAsync(id, pageDto);
                return Ok("Page updated successfully!");
            }
            catch (ServiceHttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("page/{id}")]
        [Authorize(Policy = PolicyConstant.SystemAdminOnly)]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            try
            {
                await _pageService.DeletePageAsync(id);
                return Ok("Page deleted successfully!");
            }
            catch (ServiceHttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 