using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotNetTask.API.Entities;

namespace dotNetTask.API.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Gets all data about employee.
        /// </summary>
        Task<Employee> GetEmployeeAsync(Guid employeeId);

        /// <summary>
        /// Gets all employees with their data.
        /// </summary>
        Task<IEnumerable<Employee>> GetEmployeesAsync();


        /// <summary>
        /// Gets all employees by boss ID.
        /// </summary>
        Task<IEnumerable<Employee>> GetEmployeesByBossIdAsync(Guid bossId);

        /// <summary>
        /// Caunts employees and calculates average salary for particular Role.
        /// </summary>
        Task<CountAndAverage> GetCauntAndAvarageByRoleAsync(string role);

        /// <summary>
        /// Creates new employee.
        /// </summary>
        Task<Employee> AddNewEmployeeAsync(Employee employee);

        /// <summary>
        /// Updates employee with all employee data
        /// </summary>
        Task UpdateEmployeeAsync(Employee employee);

        /// <summary>
        /// Deletes employee
        /// </summary>
        Task DeleteEmployee(Guid employeeId);

        /// <summary>
        /// Checking for CEO existing 
        /// </summary>v
        Task<bool> CheckIsCeoExistAsync();

        /// <summary>
        /// Gets all employees by Name and Birtdate interval
        /// </summary>
        Task<IEnumerable<Employee>> GetEmployeesByNameAndDateIntervalAsync(string name, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Updates employee salary by ID
        /// </summary>
        Task UpdateEmployeeSalary(Guid employeeId, int salary);
    }
}
