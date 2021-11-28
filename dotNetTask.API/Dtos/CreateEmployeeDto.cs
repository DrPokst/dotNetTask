using System;
using System.ComponentModel.DataAnnotations;

namespace dotNetTask.API.Dtos
{
    public record CreateEmployeeDto
    {
        [Required]
        public string FistName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirtDate { get; set; }

        public string Boss { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [Required]
        public int CurrentSalary { get; set; }
        [Required]
        public EmployeeRoles Role { get; set; }
    }
}
