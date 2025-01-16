using EmployeeManagement.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Core.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; } = Status.Active;
        public Guid PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
