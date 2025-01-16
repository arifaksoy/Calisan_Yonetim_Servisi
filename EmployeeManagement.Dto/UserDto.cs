using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Dto
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
    }
}
