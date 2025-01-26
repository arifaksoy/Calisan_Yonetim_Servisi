using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Dto
{
    public class BulkAddTimeEntriesRequest
    {
        [Required]
        public List<TimeEntriesDto> TimeEntries { get; set; }
    }
} 