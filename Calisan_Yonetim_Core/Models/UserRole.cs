using EmployeeManagement.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Core.Models
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; } = new Guid();
        public Status Status { get; set; }
        public Guid UserId { get; set; }  // Foreign key to User
        public User User { get; set; }

        public Guid RoleId { get; set; }  // Foreign key to Role
        public Role Role { get; set; }

        
    }
}
