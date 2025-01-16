using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IAccountRepository
    {
       Task Register(PersonnelUserDto personnelUserDto);
    }
}
