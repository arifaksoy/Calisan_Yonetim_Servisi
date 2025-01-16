using EmployeeManagement.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Core.Models
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Status CompanyStatus { get; set; }

        public ICollection<User> Users { get; set; }  // Kullanıcı ilişkisi
    }
}
