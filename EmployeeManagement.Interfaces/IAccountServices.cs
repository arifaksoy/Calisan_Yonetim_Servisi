using EmployeeManagement.Dto;

namespace EmployeeManagement.Interfaces
{
    public interface IAccountServices
    {
        Task<string> Login(UserDto user);
    }
}
