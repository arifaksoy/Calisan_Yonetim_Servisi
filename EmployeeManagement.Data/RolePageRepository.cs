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
    public class RolePageRepository : IRolePageRepository
    {
        private readonly CalisanYonetimDbContext _context;
        private readonly ILogger<PageRepository> _logger;

        public RolePageRepository(CalisanYonetimDbContext context, ILogger<PageRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<RolePage> GetAll()
        {
            return _context.RolePages.AsNoTracking();
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

        public async Task<RolePage> AddAsync(RolePage rolepage)
        {
            if (rolepage == null)
                throw new ArgumentNullException(nameof(rolepage));

            try
            {
                var isExistsRolePage = await _context.RolePages.AnyAsync(rp => rp.RoleId == rolepage.RoleId && rp.PageId == rolepage.PageId);

                if (!isExistsRolePage)
                {
                    await _context.RolePages.AddAsync(rolepage);
                    await _context.SaveChangesAsync();
                }

                return rolepage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Error}",  ex.Message);
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

        public async Task DeleteAsync(Guid pageId, Guid roleId)
        {
            try
            {
                
                var rolePage = await _context.RolePages
                    .FirstOrDefaultAsync(p => p.PageId == pageId && p.RoleId ==roleId && p.Status == Status.Active);
                
                if (rolePage != null)
                {
                    rolePage.Status = Status.Inactive;
                    await _context.SaveChangesAsync();
           
                }
          
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}