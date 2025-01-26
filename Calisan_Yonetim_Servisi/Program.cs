using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using EmployeeManagement.Common.Constant;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Calisan_Yonetim_Core.Services;
using Calisan_Yonetim_Core.Extensions;
using EmployeeManagement.Services.Maps;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
builder.Services.AddAutoMapper(typeof(ProjectProfile));
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddDbContext<CalisanYonetimDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);

// Configure JWT Authentication

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IPersonnelRepository, PersonnelRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IRolePageRepository, RolePageRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITimeEntriesRepository, TimeEntriesRepository>();

// Service kayıtlarını da Scoped yapın
builder.Services.AddScoped<IAccountServices, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICompanyServices, CompanyService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IPersonnelService, PersonnelService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITimeEntriesService, TimeEntriesService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyConstant.SystemAdminOnly, policy =>
        policy.RequireClaim(TokenClaimConstants.Role, UserRoleConstant.SystemAdmin));
    options.AddPolicy(PolicyConstant.User, policy =>
        policy.RequireAssertion(context => {
            var userIdClaim = context.User.FindFirst(TokenClaimConstants.UserId)?.Value;
            var httpContext = context.Resource as DefaultHttpContext;
            var routeUserId = httpContext?.Request.RouteValues["userId"]?.ToString();
            return !string.IsNullOrEmpty(userIdClaim) && !string.IsNullOrEmpty(routeUserId) && userIdClaim == routeUserId;
        }));
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Veritabanı başlatma kodunu ekleyin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CalisanYonetimDbContext>();
        context.Database.EnsureCreated(); // Veritabanının oluşturulduğundan emin olun
        
        // Eğer migration'ları otomatik uygulamak isterseniz:
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı başlatılırken bir hata oluştu.");
    }
}
app.Run();
