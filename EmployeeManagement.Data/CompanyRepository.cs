using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Data
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(CalisanYonetimDbContext context, ILogger<CompanyRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public IQueryable<Company> GetAll()
        {
            return _context.Company.AsNoTracking();
        }

        public async Task Insert(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            try
            {
                _logger.LogInformation("Adding new company: {CompanyName}", company.CompanyName);
                await _context.Company.AddAsync(company);
                await SaveChangesAsync();
                _logger.LogInformation("Successfully added company: {CompanyName}", company.CompanyName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding company {CompanyName}: {Error}", 
                    company.CompanyName, ex.Message);
                throw;
            }
        }

        public async Task Update(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            try
            {
                _logger.LogInformation("Updating company: {CompanyName}", company.CompanyName);
                _context.Company.Update(company);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated company: {CompanyName}", company.CompanyName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company {CompanyName}: {Error}", 
                    company.CompanyName, ex.Message);
                throw;
            }
        }
    }
}
