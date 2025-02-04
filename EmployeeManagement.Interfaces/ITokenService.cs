﻿using EmployeeManagement.Common.Constant;
using EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface  ITokenService
    {
        string GenerateToken(TokenClaimDto tokenClaim);
        string GetUserRoleFromToken();
        public string GetUserCompanyIdFromToken();
        public string GetUserUserIdFromToken();
    }
}
