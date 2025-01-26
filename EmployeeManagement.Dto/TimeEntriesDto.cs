using EmployeeManagement.Common.Enums;
using System;

namespace EmployeeManagement.Dto
{
    public class TimeEntriesDto
    {
        public Guid TimeEntriesId { get; set; }
        public DateTime TimeEntriesDate { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public Guid PersonnelId { get; set; }
        public string PersonnelName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public Status Status { get; set; } = Status.Active;
    }
} 