using EmployeeManagement.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IPersonnelService
    {
        Task<IEnumerable<PersonnelListDto>> GetAllPersonnelAsync(Guid companyId);
        Task CreatePersonnelAsync(Guid companyId, PersonnelUserDto personnelUserDto);
        Task UpdatePersonnelAsync(Guid companyId, Guid employeeId, PersonnelUserDto personnelUserDto);
    }
} 