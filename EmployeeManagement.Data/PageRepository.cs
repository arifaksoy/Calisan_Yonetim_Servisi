using Calisan_Yonetim_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Interfaces;
using Microsoft.Extensions.Logging;
using EmployeeManagement.Common.Constant;

namespace EmployeeManagement.Data
{
    public class PageRepository : IPageRepository
    {
        private readonly CalisanYonetimDbContext _context;
        private readonly ILogger<PageRepository> _logger;

        public PageRepository(CalisanYonetimDbContext context, ILogger<PageRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<Page> GetAll()
        {
            return _context.Pages.AsNoTracking();
        }

        //public async Task<IEnumerable<Page>> GetPagesByRoleIdAsync(Guid roleId)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Fetching pages for role: {RoleId}", roleId);
        //        return await _context.RolePages
        //            .Where(rp => (rp.RoleId == roleId && rp.Status == Status.Active))
        //            .Include(rp => rp.Page)
        //            .Where(rp => rp.Page.Status == Status.Active)
        //            .Select(rp => rp.Page)
        //            .ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching pages for role {RoleId}", roleId);
        //        throw;
        //    }
        //}

        public async Task<Page> AddAsync(Page page)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));

            try
            {
                _logger.LogInformation("Adding new page: {PageName}", page.PageName);
                await _context.Pages.AddAsync(page);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added page: {PageName}", page.PageName);
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding page {PageName}: {Error}", 
                    page.PageName, ex.Message);
                throw;
            }
        }

        public async Task Update(Page page)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));

            try
            {
                _logger.LogInformation("Updating page: {PageName}", page.PageName);
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated page: {PageName}", page.PageName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating page {PageName}: {Error}", 
                    page.PageName, ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete page with ID: {PageId}", id);
                var page = await _context.Pages
                    .FirstOrDefaultAsync(p => p.PageId == id && p.Status == Status.Active);
                
                if (page != null)
                {
                    page.Status = Status.Inactive;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Successfully deleted page with ID: {PageId}", id);
                }
                else
                {
                    _logger.LogWarning("No active page found with ID: {PageId}", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting page with ID {PageId}: {Error}", 
                    id, ex.Message);
                throw;
            }
        }
    }
}