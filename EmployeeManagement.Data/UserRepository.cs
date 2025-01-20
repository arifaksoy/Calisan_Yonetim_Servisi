using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CalisanYonetimDbContext context) : base(context)
        {
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users.AsNoTracking();
        }

        public async Task Insert(User user)
        {
            await _context.Users.AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            if (user != null)
            {
                user.Status = Status.Inactive;
                _context.Users.Update(user);
                await SaveChangesAsync();
            }
        }
    }
}
