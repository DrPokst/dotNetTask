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
        Task<Employee> GetEmployeeAsync(int employeeId);

        /// <summary>
        /// Gets all employees with their data.
        /// </summary>
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        /// <summary>
        /// Gets all employees by boss ID.
        /// </summary>
        Task<IEnumerable<Employee>> GetEmployeesByBossIdAsync(int bossId);

        /// <summary>
        /// Caunts employees and calculates average salary for particular Role.
        /// </summary>
        Task<int> GetCauntAndAvarageByRoleAsync(string role);

        Task<Employee> UserExists(string user);
        Task<Employee> AddNewEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee editedEmployee);
        Task<bool> DeleteEmployee(int employeeId);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
    }
}
