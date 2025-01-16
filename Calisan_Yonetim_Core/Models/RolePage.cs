using EmployeeManagement.Common.Enums;
using System;

namespace Calisan_Yonetim_Core.Models
{
    public class RolePage
    {
        public Guid RolePageId { get; set; } = Guid.NewGuid();
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid PageId { get; set; }
        public Page Page { get; set; }
        public Status Status { get; set; }
    }
} 