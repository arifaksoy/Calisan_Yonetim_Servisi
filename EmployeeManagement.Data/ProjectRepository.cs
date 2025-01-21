using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly CalisanYonetimDbContext _context;

        public ProjectRepository(CalisanYonetimDbContext context)
        {
            _context = context;
        }


        public IQueryable<Project> GetAll()
        {
            return _context.Projects.AsNoTracking();
        }

        public async Task AddAsync(Project project)
        {
            if(project == null) throw new ArgumentNullException(nameof(project));

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }
        public async Task<Project> UpdateAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteAsync(Guid id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p=> p.ProjectId == id && p.Status == Status.Active);
            
            if (project != null)
            {
                project.Status = Status.Inactive;
                await _context.SaveChangesAsync();
            }
        }
    }
} 