using Calisan_Yonetim_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IPageRepository
    {
        IQueryable<Page> GetAll();
        //Task<IEnumerable<Page>> GetPagesByRoleIdAsync(Guid roleId);
        Task<Page> AddAsync(Page page);
        Task Update(Page page);
        Task DeleteAsync(Guid id);
    }
}