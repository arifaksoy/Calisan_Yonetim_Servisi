using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Constant;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Services;
public class TokenService:ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GenerateToken(TokenClaimDto tokenClaim)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, tokenClaim.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(TokenClaimConstants.Role, tokenClaim.RoleName),
            new Claim(TokenClaimConstants.CompanyId, tokenClaim.CompanyId.ToString()),
            new Claim(TokenClaimConstants.PersonnelId, tokenClaim.PersonnelId.ToString()),
            new Claim(TokenClaimConstants.UserId, tokenClaim.UserId.ToString()),
            new Claim(TokenClaimConstants.CompanyName, tokenClaim.CompanyName),
            new Claim(TokenClaimConstants.FullName, tokenClaim.FullName),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetUserRoleFromToken()
    {
        var role = _httpContextAccessor.HttpContext?.User?.FindFirst(TokenClaimConstants.Role)?.Value;
        return role ?? string.Empty;
    }

    public string GetUserCompanyIdFromToken()
    {
        var companyId = _httpContextAccessor.HttpContext?.User?.FindFirst(TokenClaimConstants.CompanyId)?.Value;
        return companyId ?? string.Empty;
    }

    public string GetUserUserIdFromToken()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(TokenClaimConstants.UserId)?.Value;
        return userId ?? string.Empty;
    }
}
