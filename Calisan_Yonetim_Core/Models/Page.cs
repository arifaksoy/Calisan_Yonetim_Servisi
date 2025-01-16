using EmployeeManagement.Common.Enums;
using System;

namespace Calisan_Yonetim_Core.Models
{
    public class Page
    {
        public Guid PageId { get; set; } = Guid.NewGuid();
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        public Status Status { get; set; } = Status.Active;
    }
} 