using System;
using EmployeeManagement.Common.Enums;

namespace Calisan_Yonetim_Core.Models
{
    public class TimeEntries
    {
        public Guid TimeEntriesId { get; set; }
        public DateTime TimeEntriesDate { get; set; }
        public Guid ProjectId { get; set; }
        public Guid PersonnelId { get; set; }
        public decimal Amount { get; set; }
        public Status Status { get; set; }

        // Navigation properties
        public virtual Project Project { get; set; }
        public virtual Personnel Personnel { get; set; }
    }
} 