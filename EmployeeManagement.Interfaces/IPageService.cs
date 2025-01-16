using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Core.Services
{
    public interface IPageService
    {
        Task<IEnumerable<PageDto>> GetPagesAsync(Guid userId);
        Task<Page> CreatePageAsync(PageDto page);
        Task UpdatePageAsync(Guid pageId, PageDto pageDto);
        Task DeletePageAsync(Guid id);
    }
} 