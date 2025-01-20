using System;

namespace EmployeeManagement.Dto
{
    public class PersonnelListDto
    {
        public Guid PersonnelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid UserCompanyId { get; set; }
        public string CompanyName { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
} 