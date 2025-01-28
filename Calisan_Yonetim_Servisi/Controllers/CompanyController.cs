using Calisan_Yonetim_Core.Exceptions;
using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Constant;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Calisan_Yonetim_Servisi.Controllers
{
    [Authorize(Policy = PolicyConstant.AdminOrSystemAdmin)]
    [Route("api/v1/Company")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ICompanyServices _CompanyServices;

        public CompanyController(IConfiguration configuration, ICompanyServices CompanyServices)
        {
            _configuration = configuration;
            _CompanyServices = CompanyServices;
        }

        [HttpPost("add-company")]
        public async Task<IActionResult> AddCompany([FromBody] CompanyDto company)
        {
            await _CompanyServices.AddNewCompany(company);
            return Ok("Added company successfully!");
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] CompanyDto company)
        {
            try
            {
                await _CompanyServices.UpdateCompany(companyId, company);
                return Ok("Company updated successfully!");
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

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            try
            {
                await _CompanyServices.DeleteCompany(companyId);
                return Ok("Company deleted successfully!");
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


        [HttpGet("get-companies")]
        public async Task<IActionResult> GetActiveCompanies()
        {
            try
            {
                var activeCompanies = await _CompanyServices.GetActiveCompanies();
                return Ok(activeCompanies);
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
