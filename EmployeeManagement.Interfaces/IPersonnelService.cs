using EmployeeManagement.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IPersonnelService
    {
        Task<IEnumerable<PersonnelListDto>> GetAllPersonnelAsync(Guid companyId);
    }
} 