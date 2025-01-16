using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Dto
{
    public class PageDto
    {
        public Guid PageId { get; set; } = Guid.NewGuid();
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        public List<RoleDto> Roles { get; set; }
        public Status Status { get; set; } = Status.Active;
    }
}
