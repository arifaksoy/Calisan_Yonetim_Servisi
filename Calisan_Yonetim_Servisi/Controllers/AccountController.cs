using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace Calisan_Yonetim_Servisi.Controllers
{
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountServices _accountServices;

        public AccountController (IConfiguration configuration,IAccountServices accountServices)
        {
            _configuration = configuration;
            _accountServices = accountServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            var token = await _accountServices.Login(user);
            return Ok(new {Token = token});
        }
    }
}
