using Calisan_Yonetim_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface ITimeEntriesRepository
    {
        IQueryable<TimeEntries> GetAll();
        Task AddAsync(TimeEntries timeEntry);
        Task BulkAddAsync(List<TimeEntries> timeEntries);
        Task<TimeEntries> UpdateAsync(TimeEntries timeEntry);
        Task DeleteAsync(Guid id);
        Task BulkDeleteAsync(List<Guid> ids);
    }
} 