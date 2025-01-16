using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CalisanYonetimDbContext _context;

        public AccountRepository(CalisanYonetimDbContext context)
        {
            _context = context;
        }
        public async Task Register(PersonnelUserDto personnelUserDto)
        {
            try
            {
                    var newPersonnel = new Personnel
                    {
                        FistName = personnelUserDto.Personnel.FirstName,
                        LastName = personnelUserDto.Personnel.LastName,
                        Email = personnelUserDto.Personnel.Email,
                        Status = Status.Active,
                    };

                    _context.Personnel.Add(newPersonnel);
             

                var newUser = new User
                    {
                        Username = personnelUserDto.User.Username,
                        Password = personnelUserDto.User.Password,
                        Status = Status.Active,
                        PersonnelId = newPersonnel.PersonnelId
                    };

                    _context.Users.Add(newUser);
                  

                    var adminRoleId = _context.Role.FirstOrDefault(r => r.RoleName == "Admin")?.RoleId;
                    var userRoleId = _context.Role.FirstOrDefault(r => r.RoleName == "User")?.RoleId;
                    var userCount = _context.Users.Count();

                    var userRole = new UserRole
                    {
                        RoleId = userCount == 1 ? adminRoleId.Value : userRoleId.Value,
                        UserId = newUser.Id,
                        Status = Status.Active
                    };

                    _context.UserRole.Add(userRole);
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //public async Task<User?> GetUserAsync(UserDto user)
        //{
        //    return await _context.Users.Where(u=>u.Username == user.Username && u.Password == user.Password).FirstOrDefaultAsync();
        //}
    }
}
