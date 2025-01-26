using EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface ITimeEntriesService
    {
        Task<List<TimeEntriesDto>> GetTimeEntries(Guid personnelId, DateTime? startDate = null, DateTime? endDate = null);
        Task BulkAddAsync(List<TimeEntriesDto> timeEntries);
        Task UpdateAsync(Guid timeEntryId, TimeEntriesDto timeEntriesDto);
        Task DeleteAsync(Guid id);
        Task BulkDeleteAsync(List<Guid> ids);
    }
} 