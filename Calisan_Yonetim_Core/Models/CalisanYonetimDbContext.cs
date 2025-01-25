using EmployeeManagement.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Core.Models
{
    public class CalisanYonetimDbContext : DbContext
    {
        // Define constant GUIDs for seed data
        private static readonly Guid DEFAULT_COMPANY_ID = new Guid("71FCDEF1-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid DEFAULT_PERSONNEL_ID = new Guid("71FCDEF2-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid DEFAULT_USER_ID = new Guid("71FCDEF3-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid SYSTEM_ADMIN_ROLE_ID = new Guid("71FCDEF4-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid COMPANY_PAGE_ID = new Guid("71FCDEF5-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid PAGES_PAGE_ID = new Guid("71FCDEF6-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid ADMIN_ROLE_ID = new Guid("71FCDEF7-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid USER_ROLE_ID = new Guid("71FCDEF8-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid DEFAULT_USER_ROLE_ID = new Guid("71FCDEF9-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid COMPANY_ROLE_PAGE_ID = new Guid("71FCDEFA-D547-49DB-8A4D-C441E959C0F4");
        private static readonly Guid PAGES_ROLE_PAGE_ID = new Guid("71FCDEFB-D547-49DB-8A4D-C441E959C0F4");

        public CalisanYonetimDbContext(DbContextOptions<CalisanYonetimDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<RolePage> RolePages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeEntries> TimeEntries { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Personnel)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonnelId);

            // TimeEntries relationships
            modelBuilder.Entity<TimeEntries>()
                .HasOne(te => te.Project)
                .WithMany()
                .HasForeignKey(te => te.ProjectId);

            modelBuilder.Entity<TimeEntries>()
                .HasOne(te => te.Personnel)
                .WithMany()
                .HasForeignKey(te => te.PersonnelId);

            // TimeEntries unique index
            modelBuilder.Entity<TimeEntries>()
                .HasIndex(te => new { te.TimeEntriesDate, te.ProjectId, te.PersonnelId })
                .IsUnique();

            // Project ve Company ilişkisi
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(p => p.CompanyId);

            // Yeni Foreign Key için tanımlama
            modelBuilder.Entity<User>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId);

            modelBuilder.Entity<Personnel>()
               .HasOne(p => p.User)
               .WithOne(u => u.Personnel)
               .HasForeignKey<User>(u => u.PersonnelId);

            modelBuilder.Entity<UserRole>()
              .HasOne(ur => ur.Role)
              .WithMany() // Assuming one role can have multiple users
              .HasForeignKey(ur => ur.RoleId);

            // Company Seed
            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyId = DEFAULT_COMPANY_ID,
                CompanyName = "Pau",
                CompanyStatus = Status.Active
            });

            // Personnel Seed
            modelBuilder.Entity<Personnel>().HasData(new Personnel
            {
                PersonnelId = DEFAULT_PERSONNEL_ID,
                FistName = "Admin",
                LastName = "Pau",
                Email = "admin@pau.net",
                Status = Status.Active
            });

            // Role Seed
            modelBuilder.Entity<Role>().HasData(
                 new Role
                 {
                     RoleId = SYSTEM_ADMIN_ROLE_ID,
                     RoleName = "System Admin",
                     Status = Status.Active
                 },
                new Role
                {
                    RoleId = ADMIN_ROLE_ID,
                    RoleName = "Admin",
                    Status = Status.Active
                },
                new Role
                {
                    RoleId = USER_ROLE_ID,
                    RoleName = "User",
                    Status = Status.Active
                }
            );

            // User Seed
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = DEFAULT_USER_ID,
                Username = "admin",
                Password = "admin123",
                PersonnelId = DEFAULT_PERSONNEL_ID,
                CompanyId = DEFAULT_COMPANY_ID,
                Status = Status.Active
            });

            // UserRole Seed
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                UserRoleId = DEFAULT_USER_ROLE_ID,
                UserId = DEFAULT_USER_ID,
                RoleId = SYSTEM_ADMIN_ROLE_ID,
                Status = Status.Active,
            });

            // Page Seed
            modelBuilder.Entity<Page>().HasData(
                new Page
                {
                    PageId = COMPANY_PAGE_ID,
                    PageName = "Company",
                    PageDescription = "",
                    Status = Status.Active
                },
                new Page
                {
                    PageId = PAGES_PAGE_ID,
                    PageName = "Pages",
                    PageDescription = "",
                    Status = Status.Active
                }
            );

            // RolePage mappings
            modelBuilder.Entity<RolePage>().HasData(
                new RolePage
                {
                    RolePageId = COMPANY_ROLE_PAGE_ID,
                    RoleId = SYSTEM_ADMIN_ROLE_ID,
                    PageId = COMPANY_PAGE_ID,
                    Status = Status.Active
                },
                new RolePage
                {
                    RolePageId = PAGES_ROLE_PAGE_ID,
                    RoleId = SYSTEM_ADMIN_ROLE_ID,
                    PageId = PAGES_PAGE_ID,
                    Status = Status.Active
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
