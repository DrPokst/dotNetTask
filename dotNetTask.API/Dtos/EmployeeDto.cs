﻿using System;
using dotNetTask.API.Entities;

namespace dotNetTask.API.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; init; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public DateTime BirtDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Boss { get; set; }
        public string HomeAddress { get; set; }
        public int CurrentSalary { get; set; }
        public EmployeeRoles Role { get; set; }
    }
}
