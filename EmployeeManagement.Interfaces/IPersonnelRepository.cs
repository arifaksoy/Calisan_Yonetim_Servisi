using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IPersonnelRepository 
    {
        IQueryable<Personnel> GetAll();
        Task Insert(Personnel personnel);
        Task Update(Personnel personnel);
        Task<IEnumerable<PersonnelListDto>> GetAllActivePersonnelAsync();
    }
}
