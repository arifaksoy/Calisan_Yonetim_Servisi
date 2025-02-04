﻿using Calisan_Yonetim_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        Task Insert(User user);
        Task Update(User user);
        Task Delete(User user);
    }
}
