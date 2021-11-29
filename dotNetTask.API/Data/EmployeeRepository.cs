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

        public async Task<Employee> GetEmployeeAsync(Guid employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByBossIdAsync(Guid bossId)
        {
            return await _context.Employees.Where(u => u.Boss.Id == bossId).ToListAsync();
        }

        public async Task<Employee> AddNewEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployee(Guid employeeId)
        {
            var employeeToDelete = await _context.Employees.FindAsync(employeeId);
            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<CountAndAverage> GetCauntAndAvarageByRoleAsync(string role)
        {
            var roleId = (EmployeeRoles)Enum.Parse(typeof(EmployeeRoles), role); 
            var employeesByRole = await _context.Employees.Where(u => u.Role == roleId).ToListAsync();

            CountAndAverage countAndAverage = new()
            {
                Role = role,
                Count = employeesByRole.Count(),
                Average = employeesByRole.Average(u => u.CurrentSalary)
            };

            return countAndAverage;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var employeeToUpdate = await _context.Employees.FindAsync(employee.Id);
            employeeToUpdate = employee;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIsCeoExistAsync()
        {
            var ceo = await _context.Employees.FirstOrDefaultAsync(u => u.Role == EmployeeRoles.CEO);
            
            if (ceo is null) return false;
            
            return true;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByNameAndDateIntervalAsync(string name, DateTime startDate, DateTime endDate)
        {
            var employeesByName = await _context.Employees.Where(u => u.FirstName == name).ToListAsync();

            if(startDate != default(DateTime)) employeesByName = await _context.Employees.Where(u => u.FirstName == name).Where(u => u.BirtDate > startDate).ToListAsync();
            if(endDate != default(DateTime)) employeesByName = await _context.Employees.Where(u => u.FirstName == name).Where(u => u.BirtDate < endDate).ToListAsync();

            return employeesByName;
        }

        public async Task UpdateEmployeeSalary(Guid employeeId, int salary)
        {
            var employeefromRepository = await _context.Employees.FindAsync(employeeId);
            employeefromRepository.CurrentSalary = salary;
            await _context.SaveChangesAsync();
        }
    }
}
