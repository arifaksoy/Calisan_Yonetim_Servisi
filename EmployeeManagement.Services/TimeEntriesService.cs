using AutoMapper;
using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Calisan_Yonetim_Core.Exceptions;

namespace EmployeeManagement.Services
{
    public class TimeEntriesService : ITimeEntriesService
    {
        private readonly ITimeEntriesRepository _timeEntriesRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IPersonnelRepository _personnelRepository;

        public TimeEntriesService(
            ITimeEntriesRepository timeEntriesRepository, 
            ITokenService tokenService, 
            IMapper mapper,
            IProjectRepository projectRepository,
            IPersonnelRepository personnelRepository)
        {
            _timeEntriesRepository = timeEntriesRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _personnelRepository = personnelRepository;
        }

        public async Task<List<TimeEntriesDto>> GetTimeEntries(Guid personnelId, Guid projectId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _timeEntriesRepository.GetAll()
                .Include(t => t.Project)
                .Include(t => t.Personnel)
                .Where(t => t.Status == Status.Active);

            if (startDate.HasValue)
            {
                query = query.Where(t => t.TimeEntriesDate >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.TimeEntriesDate <= endDate.Value.Date);
            }

            if(personnelId != Guid.Empty)
            {
                query = query.Where(t=> t.PersonnelId == personnelId);
            }

            if (projectId != Guid.Empty)
            {
                query = query.Where(t => t.ProjectId == projectId);
            }

            var timeEntries = await query.ToListAsync();
            return _mapper.Map<List<TimeEntriesDto>>(timeEntries);
        }

        public async Task BulkAddAsync(List<TimeEntriesDto> timeEntries)
        {
            if (timeEntries == null || !timeEntries.Any())
            {
                throw new ServiceHttpException(HttpStatusCode.BadRequest, "No time entries provided");
            }

            // Validate all projects exist and are active
            var projectIds = timeEntries.Select(t => t.ProjectId).Distinct().ToList();
            var existingProjects = await _projectRepository.GetAll()
                .Where(p => projectIds.Contains(p.ProjectId) && p.Status == Status.Active)
                .Select(p => p.ProjectId)
                .ToListAsync();

            var invalidProjectIds = projectIds.Except(existingProjects).ToList();
            if (invalidProjectIds.Any())
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, $"Projects not found or inactive: {string.Join(", ", invalidProjectIds)}");
            }

            // Validate all personnel exist and are active
            var personnelIds = timeEntries.Select(t => t.PersonnelId).Distinct().ToList();
            var existingPersonnel = await _personnelRepository.GetAll()
                .Where(p => personnelIds.Contains(p.PersonnelId) && p.Status == Status.Active)
                .Select(p => p.PersonnelId)
                .ToListAsync();

            var invalidPersonnelIds = personnelIds.Except(existingPersonnel).ToList();
            if (invalidPersonnelIds.Any())
            {
                throw new ServiceHttpException(HttpStatusCode.NotFound, $"Personnel not found or inactive: {string.Join(", ", invalidPersonnelIds)}");
            }

            // Create a list of composite keys for new entries
            var newEntryKeys = timeEntries.Select(t => new 
            { 
                t.ProjectId, 
                t.PersonnelId, 
                t.TimeEntriesDate 
            }).ToList();

            // Get all inactive entries that match the composite keys
            var inactiveEntries = await _timeEntriesRepository.GetAll()
                .Where(t => t.Status == Status.Inactive)
                .ToListAsync();

            var inactiveEntriesToUpdate = inactiveEntries
                .Where(ie => newEntryKeys.Any(ne => 
                    ne.ProjectId == ie.ProjectId && 
                    ne.PersonnelId == ie.PersonnelId && 
                    ne.TimeEntriesDate.Date == ie.TimeEntriesDate.Date))
                .ToList();

            // Update inactive entries
            foreach (var inactiveEntry in inactiveEntriesToUpdate)
            {
                var matchingEntry = timeEntries.First(t => 
                    t.ProjectId == inactiveEntry.ProjectId && 
                    t.PersonnelId == inactiveEntry.PersonnelId && 
                    t.TimeEntriesDate.Date == inactiveEntry.TimeEntriesDate.Date);

                var updatedEntry = _mapper.Map<TimeEntries>(matchingEntry);
                updatedEntry.TimeEntriesId = inactiveEntry.TimeEntriesId;
                await _timeEntriesRepository.UpdateAsync(updatedEntry);

                // Remove from the list to be inserted
                timeEntries.Remove(matchingEntry);
            }

            // Insert new entries
            if (timeEntries.Any())
            {
                var newEntries = timeEntries.Select(te =>
                {
                    var entry = _mapper.Map<TimeEntries>(te);
                    entry.TimeEntriesId = Guid.NewGuid();
                    return entry;
                }).ToList();

                await _timeEntriesRepository.BulkAddAsync(newEntries);
            }
        }

        public async Task UpdateAsync(Guid timeEntryId, TimeEntriesDto timeEntriesDto)
        {
            var timeEntry = _mapper.Map<TimeEntries>(timeEntriesDto);
            timeEntry.TimeEntriesId = timeEntryId;
            await _timeEntriesRepository.UpdateAsync(timeEntry);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _timeEntriesRepository.DeleteAsync(id);
        }

        public async Task BulkDeleteAsync(List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                throw new ServiceHttpException(HttpStatusCode.BadRequest, "No time entry IDs provided for deletion");
            }

            await _timeEntriesRepository.BulkDeleteAsync(ids);
        }
    }
} 