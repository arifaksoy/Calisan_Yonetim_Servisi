using Calisan_Yonetim_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IRolePageRepository
    {
        IQueryable<RolePage> GetAll();
        Task<RolePage> AddAsync(RolePage rolepage);

        Task DeleteAsync(Guid pageId, Guid roleId);
    }
}
