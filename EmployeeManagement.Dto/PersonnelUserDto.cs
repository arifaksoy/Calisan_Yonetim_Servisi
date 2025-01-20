using Calisan_Yonetim_Core.Models;

namespace EmployeeManagement.Dto
{
    public class PersonnelUserDto
    {
        public PersonnelDto Personnel { get; set; }
        public UserDto User { get; set; }
        public RoleDto Role { get; set; }
    }
}
