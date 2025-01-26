using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public class TimeEntriesRepository : ITimeEntriesRepository
    {
        private readonly CalisanYonetimDbContext _context;

        public TimeEntriesRepository(CalisanYonetimDbContext context)
        {
            _context = context;
        }

        public IQueryable<TimeEntries> GetAll()
        {
            return _context.TimeEntries.AsNoTracking();
        }

        public async Task AddAsync(TimeEntries timeEntry)
        {
            if(timeEntry == null) throw new ArgumentNullException(nameof(timeEntry));

            await _context.TimeEntries.AddAsync(timeEntry);
            await _context.SaveChangesAsync();
        }

        public async Task BulkAddAsync(List<TimeEntries> timeEntries)
        {
            if (timeEntries == null || !timeEntries.Any())
                throw new ArgumentNullException(nameof(timeEntries));

            await _context.TimeEntries.AddRangeAsync(timeEntries);
            await _context.SaveChangesAsync();
        }

        public async Task<TimeEntries> UpdateAsync(TimeEntries timeEntry)
        {
            _context.Entry(timeEntry).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return timeEntry;
        }

        public async Task DeleteAsync(Guid id)
        {
            var timeEntry = await _context.TimeEntries.FirstOrDefaultAsync(t => t.TimeEntriesId == id && t.Status == Status.Active);
            
            if (timeEntry != null)
            {
                timeEntry.Status = Status.Inactive;
                await _context.SaveChangesAsync();
            }
        }

        public async Task BulkDeleteAsync(List<Guid> ids)
        {
            var timeEntries = await _context.TimeEntries
                .Where(t => ids.Contains(t.TimeEntriesId) && t.Status == Status.Active)
                .ToListAsync();

            foreach (var entry in timeEntries)
            {
                entry.Status = Status.Inactive;
            }

            await _context.SaveChangesAsync();
        }
    }
} 