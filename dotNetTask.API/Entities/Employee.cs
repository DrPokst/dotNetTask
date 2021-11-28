using System;
namespace dotNetTask.API.Entities
{
    public record Employee
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirtDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public Employee Boss { get; set; }
        public string HomeAddress { get; set; }
        public int CurrentSalary { get; set; }
        public EmployeeRoles Role { get; set; }
    }
}
