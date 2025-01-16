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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(CalisanYonetimDbContext context) : base(context)
        {
        }

        public IQueryable<Role> GetAll()
        {
            return _context.Role.AsNoTracking();
        }

        public async Task Insert(Role role)
        {
            await _context.Role.AddAsync(role);
            await SaveChangesAsync();
        }
    }
}
