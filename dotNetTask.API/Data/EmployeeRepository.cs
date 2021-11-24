using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetTask.API.Entities;
using dotNetTask.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotNetTask.API.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByBossIdAsync(int bossId)
        {
            return await _context.Employees.Where(u => u.Boss.Id == bossId).ToListAsync();
        }

        public Task<Employee> AddNewEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCauntAndAvarageByRoleAsync(string role)
        {
            var roleId = (EmployeeRoles)Enum.Parse(typeof(EmployeeRoles), role); // ?????
            var employeesByRole = await _context.Employees.Where(u => u.Role == roleId).ToListAsync();
            var employeeCount = employeesByRole.Count();
            var averageSalary = employeesByRole.Average(u => u.CurrentSalary);
            return employeeCount;
        }

        

        

        

        public Task<Employee> UpdateEmployee(Employee editedEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UserExists(string user)
        {
            throw new NotImplementedException();
        }

        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
