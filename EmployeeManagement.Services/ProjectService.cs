using AutoMapper;
using Calisan_Yonetim_Core.Exceptions;
using Calisan_Yonetim_Core;
using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Constant;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITokenService _tokenService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, ITokenService tokenService, IMapper mapper, ICompanyRepository companyRepository)
        {
            _projectRepository = projectRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<List<ProjectDto>> GetProjects(Guid companyId)
        {
            var currentRole = _tokenService.GetUserRoleFromToken();

            var projects =  await _projectRepository.GetAll().Include(c=>c.Company).Where(p => (p.CompanyId == companyId && p.Status == Status.Active) || currentRole == UserRoleConstant.SystemAdmin).ToListAsync();

            return _mapper.Map<List<ProjectDto>>(projects);
        }
        public async Task AddAsync(ProjectDto projectDto)
        {
            var company = await _companyRepository.GetAll().FirstOrDefaultAsync(c=>c.CompanyId == projectDto.CompanyId && c.CompanyStatus == Status.Active);

            if(company == null)
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, ErrorMessageKeys.ErrorNotFoundCompany);
            }
            var project = _mapper.Map<Project>(projectDto);
            project.ProjectId = Guid.NewGuid();
           await _projectRepository.AddAsync(project);
        }

        public async Task UpdateAsync(Guid projectId,ProjectDto projectDto)
        {
            var isExistsProject = await _projectRepository.GetAll().AnyAsync(p => p.ProjectId == projectId && p.Status == Status.Active);
            if (!isExistsProject)
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, ErrorMessageKeys.ErrorNotFoundProject);
            }
            var project = _mapper.Map<Project>(projectDto);
            project.ProjectId = projectId;
            await _projectRepository.UpdateAsync(project);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
} 