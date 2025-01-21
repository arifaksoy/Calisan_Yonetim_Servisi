using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetProjects(Guid companyId);
        Task AddAsync(ProjectDto projectDto);
        Task UpdateAsync(Guid projectId, ProjectDto projectDto);
        Task DeleteAsync(Guid id);
    }
} 