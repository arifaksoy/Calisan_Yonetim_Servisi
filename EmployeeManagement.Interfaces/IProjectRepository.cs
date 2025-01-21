using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetAll();
        Task AddAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task DeleteAsync(Guid id);
    }
} 