using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dotNetTask.API.Dtos;
using dotNetTask.API.Entities;
using dotNetTask.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotNetTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeAsync(Guid id)
        {
            var employeeForRepository = await _employeeRepository.GetEmployeeAsync(id);
            return Ok(_mapper.Map<EmployeeDto>(employeeForRepository));
            
        }
        //pagal intervala    

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesAsync()
        {
            var employeesFromRepository = await _employeeRepository.GetEmployeesAsync();
            if (employeesFromRepository is null) return NotFound();
            
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employeesFromRepository));
        }

        [HttpGet("ByBossId/{bossId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByBossIdAsync(Guid bossId)
        {
            var employeeFromrepository = await _employeeRepository.GetEmployeesByBossIdAsync(bossId);
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employeeFromrepository));
        }

        [HttpGet("/CountAndAvarage/{role}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetCauntAndAvarageByRoleAsync(string role)
        {
            var employeeFromrepository = await _employeeRepository.GetCauntAndAvarageByRoleAsync(role);
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employeeFromrepository));
            
        }

        // POST api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            Employee employeeToCreate = new()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirtDate = employeeDto.BirtDate,
                EmploymentDate = employeeDto.EmploymentDate,
                Boss = null,
                HomeAddress = employeeDto.HomeAddress,
                CurrentSalary = employeeDto.CurrentSalary,
                Role = employeeDto.Role
            };
            var createdEmployee = await _employeeRepository.AddNewEmployeeAsync(employeeToCreate);
            var createdEmployeeToReturn = _mapper.Map<EmployeeDto>(createdEmployee);
            return Ok(createdEmployeeToReturn);
        }

        // PUT api/employee/
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto employeeDto)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeAsync(id);
            if (existingEmployee is null) return NotFound();
           
            var updatedEmployee = _mapper.Map(employeeDto, existingEmployee);
            await _employeeRepository.UpdateEmployeeAsync(updatedEmployee);
            return Ok();
        }

        [HttpPatch]
        public ActionResult UpdateEmployeeSalary()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteEmployee(id);
            return Ok();
        }
    }
}
 