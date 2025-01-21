using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Common.Enums;
using System;

namespace EmployeeManagement.Dto
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } =string.Empty;
        public Status Status { get; set; } = Status.Active;
    }
} 