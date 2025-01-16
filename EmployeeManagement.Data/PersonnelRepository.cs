using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public class PersonnelRepository : BaseRepository<Personnel>, IPersonnelRepository
    {
        private readonly CalisanYonetimDbContext _context;

        public PersonnelRepository(CalisanYonetimDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Personnel> GetAll()
        {
            return _context.Personnel.AsNoTracking();
        }

        public async Task Insert(Personnel personnel)
        {
            await _context.Personnel.AddAsync(personnel);
            await SaveChangesAsync();
        }
        public async Task<IEnumerable<PersonnelListDto>> GetAllActivePersonnelAsync()
        {
            return null;
        }
    }
}




