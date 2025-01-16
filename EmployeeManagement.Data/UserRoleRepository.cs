using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(CalisanYonetimDbContext context) : base(context)
        {
        }

        public IQueryable<UserRole> GetAll()
        {
            return _context.UserRole.AsNoTracking();
        }

        public async Task Insert(UserRole userRole)
        {
            await _context.UserRole.AddAsync(userRole);
            await SaveChangesAsync();
        }
    }
}
