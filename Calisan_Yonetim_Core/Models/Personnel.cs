using EmployeeManagement.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Core.Models
{
    public class Personnel
    {
        public Guid PersonnelId { get; set; } = Guid.NewGuid();
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Status Status { get; set; }=Status.Active;
        public User User { get; set; }
    }
}
