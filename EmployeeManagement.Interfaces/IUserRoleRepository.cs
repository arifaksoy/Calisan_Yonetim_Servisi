using Calisan_Yonetim_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IUserRoleRepository
    {
        IQueryable<UserRole> GetAll();
        Task Insert(UserRole userRole);
    }
}
