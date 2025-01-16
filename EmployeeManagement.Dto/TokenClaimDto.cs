using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Dto
{
    public class TokenClaimDto
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public Guid CompanyId { get; set; }
        public Guid PersonnelId { get; set; }
        public Guid UserId { get; set; }
        public string CompanyName { get; set; }
        public string FullName { get; set; }
    }
}
