using System;
using System.ComponentModel.DataAnnotations;
using dotNetTask.API.Helpers;

namespace dotNetTask.API.Dtos
{
    public record CreateEmployeeDto
    {
        [Required]
        [MaxLength(50),MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50),MinLength(1)]
        [NotEqual(nameof(FirstName), ErrorMessage ="LastName can not be equal to Firstname")]
        public string LastName { get; set; }
        [Required]
        [AgeRange(18,70)]
        public DateTime BirtDate { get; set; }
        [Required]
        [ValidDate("2000/01/01", ErrorMessage = "Employment date can not be futute date and earlier than 2000-01-01")]
        public DateTime EmploymentDate { get; set; }
        public Guid BossId { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int CurrentSalary { get; set; }
        [Required]
        public EmployeeRoles Role { get; set; }
    }
}
