using EmployeeManagement.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calisan_Yonetim_Core.Models
{
    public class Project
    {
        [Key]
        public Guid ProjectId { get; set; }
        
        [Required]
        public string ProjectName { get; set; }
        
        public string ProjectDescription { get; set; }
        
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        
        public virtual Company Company { get; set; }
        
        public Status Status { get; set; }
    }
} 