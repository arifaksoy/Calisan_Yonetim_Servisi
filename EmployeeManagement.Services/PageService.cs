using Calisan_Yonetim_Core.Exceptions;
using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EmployeeManagement.Dto;
using EmployeeManagement.Services;
using EmployeeManagement.Common.Constant;
using System.Data;

namespace Calisan_Yonetim_Core.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<PageService> _logger;

        public PageService(
            IPageRepository pageRepository,
            IUserRoleRepository userRoleRepository,
            IRolePageRepository rolePageRepository,
            ILogger<PageService> logger,
            ITokenService tokenService,
            IRoleRepository roleRepository
            )
        {
            _pageRepository = pageRepository;
            _userRoleRepository = userRoleRepository;
            _logger = logger;
            _tokenService = tokenService;
            _rolePageRepository = rolePageRepository;
            _roleRepository = roleRepository;
        }


        public async Task<IEnumerable<PageDto>> GetPagesAsync(Guid userId)
        {
            try
            {
                _logger.LogInformation("Getting pages for user: {UserId}", userId);
                var pageDto = new List<PageDto>();
                var role = _tokenService.GetUserRoleFromToken();

                if (role == UserRoleConstant.SystemAdmin)
                {

                    var pages = await _pageRepository.GetAll().Where(p => p.Status == Status.Active)
                    .ToListAsync();

                    foreach (var page in pages)
                    {
                        pageDto.Add(new PageDto
                        {
                            PageId = page.PageId,
                            PageDescription = page.PageDescription,
                            PageName = page.PageName,
                            Roles = await _rolePageRepository.GetAll().Include(rp => rp.Role).Where(rp => rp.PageId == page.PageId && rp.Status == Status.Active).Select(x => new RoleDto { RoleId = x.Role.RoleId , RoleName = x.Role.RoleName} ).ToListAsync(),
                            Status = page.Status,
                        });
                    };

                    return pageDto;
                }

                var userRole = await _userRoleRepository.GetAll()
                   .Where(ur => ur.UserId == userId && ur.Status == Status.Active)
                   .FirstOrDefaultAsync();

                if (userRole == null)
                {
                    _logger.LogWarning("No active role found for user: {UserId}", userId);
                    throw new ServiceHttpException(HttpStatusCode.NotFound, ErrorMessageKeys.ErrorNotFoundUser);
                }

                return await _pageRepository.GetAll()
                       .Where(p => p.Status == Status.Active)
                       .Join(_rolePageRepository.GetAll(),
                            p => p.PageId,
                            rp => rp.PageId,  // UserRole tablosundaki UserId ile
                            (p, rp) => new { p, rp }
                       )
                       .Where(p=>p.rp.RoleId == userRole.RoleId && p.rp.Status == Status.Active)
                       .Select(x => new PageDto
                       {
                           PageId = x.rp.Page.PageId,
                           PageName = x.rp.Page.PageName,
                           PageDescription = x.rp.Page.PageDescription,
                       }).ToListAsync();

            }
            catch (ServiceHttpException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting pages for user {UserId}", userId);
                throw new ServiceHttpException(HttpStatusCode.InternalServerError, "Error retrieving user pages");
            }
        }

        public async Task<Page> CreatePageAsync(PageDto pageDto)
        {
            try
            {

                if (pageDto == null)
                {
                    throw new ServiceHttpException(HttpStatusCode.BadRequest, "Page data cannot be null");
                }

                if (string.IsNullOrWhiteSpace(pageDto.PageName))
                {
                    throw new ServiceHttpException(HttpStatusCode.BadRequest, "Page name is required");
                }

                _logger.LogInformation("Creating new page: {PageName}", pageDto.PageName);

                var page = new Page
                {
                    PageName = pageDto.PageName,
                    PageDescription = pageDto.PageDescription,
                    Status = Status.Active
                };
                await _pageRepository.AddAsync(page);
                foreach (var role in pageDto.Roles)
                {
                    var rolePage = new RolePage 
                    {
                        RoleId = role.RoleId,
                        PageId = page.PageId,
                        Status = Status.Active,
                    };
                    await _rolePageRepository.AddAsync(rolePage);
                }

                return page;
            }
            catch (ServiceHttpException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceHttpException(HttpStatusCode.InternalServerError, "Error creating page");
            }
        }
        //update ve delete yapıldıktan sonra react UI ayarlanacak
        public async Task UpdatePageAsync(Guid pageId, PageDto pageDto)
        {
            var page = await _pageRepository.GetAll()
                .FirstOrDefaultAsync(p => p.PageId == pageId && p.Status == Status.Active);

            var rolePage = await _rolePageRepository.GetAll().Where(p => p.PageId == pageId && p.Status == Status.Active).ToListAsync();


            if (page == null)
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, "Page not found or inactive");
            }

            page.PageName = pageDto.PageName;
            page.PageDescription = pageDto.PageDescription;

            if(pageDto.Roles.Count > 0 && rolePage.Count == 0)
            {
                foreach(var role in pageDto.Roles)
                {
                    var newRolePage = new RolePage 
                    {
                        RoleId = role.RoleId,
                        PageId = page.PageId,
                        Status = Status.Active,
                    };
                    await _rolePageRepository.AddAsync(newRolePage);
                }
            }
            else if(pageDto.Roles.Count == 0 && rolePage.Count > 0)
            {
                foreach(var pageRole in rolePage)
                {
                    await _rolePageRepository.DeleteAsync(page.PageId , pageRole.RoleId);
                }
            }
            else if(pageDto.Roles.Count > 0 && rolePage.Count > 0)
            {
                // Ekleme listesi: pageDto'da olup rolePage'de olmayanlar
                var itemsToAdd = pageDto.Roles.Where(p =>
                    !rolePage.Any(rp => p.RoleId == rp.RoleId)).ToList();

                // Silme listesi: rolePage'de olup pageDto'da olmayanlar
                var itemsToDelete = rolePage.Where(rp =>
                    !pageDto.Roles.Any(p => p.RoleId == rp.RoleId)).ToList();

                // Ekleme işlemi
                foreach (var item in itemsToAdd)
                {
                    var newRolePage = new RolePage
                    {
                        RoleId = item.RoleId,
                        PageId = page.PageId,
                        Status = Status.Active,
                    };
                    await _rolePageRepository.AddAsync(newRolePage);
                }

                // Silme işlemi
                foreach (var item in itemsToDelete)
                {
                    await _rolePageRepository.DeleteAsync(page.PageId, item.RoleId);
                }
            }

            try
            {
                await _pageRepository.Update(page);
            }
            catch (Exception ex)
            {
                throw new ServiceHttpException(HttpStatusCode.InternalServerError,
                    $"Error updating page: {ex.Message}");
            }
        }

        public async Task DeletePageAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting page: {PageId}", id);
                await _pageRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting page {PageId}", id);
                throw new ServiceHttpException(HttpStatusCode.InternalServerError, "Error deleting page");
            }
        }
    }
}