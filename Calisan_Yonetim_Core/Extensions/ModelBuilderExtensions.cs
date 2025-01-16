using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Calisan_Yonetim_Core.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static async Task SeedDataIfEmpty(this CalisanYonetimDbContext context)
        {
            await context.Database.MigrateAsync();

            if (await context.Company.AnyAsync())
            {
                return; // Eğer veri varsa hiçbir şey yapma
            }

            var defaultCompanyId = Guid.NewGuid();
            var defaultPersonnelId = Guid.NewGuid();
            var defaultUserId = Guid.NewGuid();
            var systemAdminRoleId = Guid.NewGuid();
            var companyPageId = Guid.NewGuid();
            var pagesPageId = Guid.NewGuid();

            // Company Seed
            await context.Company.AddAsync(new Company
            {
                CompanyId = defaultCompanyId,
                CompanyName = "Pau",
                CompanyStatus = Status.Active
            });

            // Personnel Seed
            await context.Personnel.AddAsync(new Personnel
            {
                PersonnelId = defaultPersonnelId,
                FistName = "Admin",
                LastName = "Pau",
                Email = "admin@pau.net",
                Status = Status.Active
            });

            // Role Seed
           

            // User Seed
            await context.Users.AddAsync(new User
            {
                Id = defaultUserId,
                Username = "admin",
                Password = "admin123",
                PersonnelId = defaultPersonnelId,
                CompanyId = defaultCompanyId,
                Status = Status.Active
            });

            // UserRole Seed
            await context.UserRole.AddAsync(new UserRole
            {
                UserRoleId = Guid.NewGuid(),
                UserId = defaultUserId,
                RoleId = systemAdminRoleId,
                Status = Status.Active
            });

            // Page Seed
            await context.Pages.AddRangeAsync(
                new Page
                {
                    PageId = companyPageId,
                    PageName = "Company",
                    PageDescription = "",
                    Status = Status.Active
                },
                new Page
                {
                    PageId = pagesPageId,
                    PageName = "Pages",
                    PageDescription = "",
                    Status = Status.Active
                }
            );

            // RolePage mappings
            await context.RolePages.AddRangeAsync(
                new RolePage
                {
                    RolePageId = Guid.NewGuid(),
                    RoleId = systemAdminRoleId,
                    PageId = companyPageId,
                    Status = Status.Active
                },
                new RolePage
                {
                    RolePageId = Guid.NewGuid(),
                    RoleId = systemAdminRoleId,
                    PageId = pagesPageId,
                    Status = Status.Active
                }
            );

            await context.SaveChangesAsync();
        }
    }
} 