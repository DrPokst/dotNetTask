using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dotNetTask.API.Dtos;
using dotNetTask.API.Entities;
using dotNetTask.API.Interfaces;
using LoggerService;
using Microsoft.AspNetCore.Mvc;

namespace dotNetTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper, ILoggerManager logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //GET api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeAsync(Guid id)
        {
            var employeeFromRepository = await _employeeRepository.GetEmployeeAsync(id);

            if (employeeFromRepository is null) return NotFound();

            return Ok(_mapper.Map<EmployeeDto>(employeeFromRepository));
        }

        // GET api/employees/ByNameAndDateInterval/{name}
        [HttpGet("ByNameAndDateInterval/{name}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByNameAndDateIntervalAsync(string name, DateTime startDate, DateTime endDate)
        {
            var employeeFromRepository = await _employeeRepository.GetEmployeesByNameAndDateIntervalAsync(name, startDate, endDate);

            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employeeFromRepository));
        } 

        //GET api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesAsync()
        {
            var employeesFromRepository = await _employeeRepository.GetEmployeesAsync();

            if (employeesFromRepository is null) return NotFound();

            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employeesFromRepository));
        }

        // GET api/employees/ByBossId/{bossId}
        [HttpGet("ByBossId/{bossId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByBossIdAsync(Guid bossId)
        {
            var employeeFromrepository = await _employeeRepository.GetEmployeesByBossIdAsync(bossId);

            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employeeFromrepository));
        }

        // GET api/employees/CountAndAverage/{role}
        [HttpGet("/CountAndAvarage/{role}")]  
        public async Task<ActionResult<CountAndAverage>> GetCauntAndAvarageByRoleAsync(string role)
        {
            var countAndAverageByRole = await _employeeRepository.GetCauntAndAvarageByRoleAsync(role);

            return Ok(countAndAverageByRole);
        }

        // POST api/employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            if (employeeDto.Role == EmployeeRoles.CEO && await _employeeRepository.CheckIsCeoExistAsync()) return BadRequest("CEO exist");

            Employee getBoss = null;
            if (employeeDto.Role != EmployeeRoles.CEO) getBoss = await _employeeRepository.GetEmployeeAsync(employeeDto.BossId);
            
            Employee employeeToCreate = new()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirtDate = employeeDto.BirtDate,
                EmploymentDate = employeeDto.EmploymentDate,
                Boss = getBoss,
                HomeAddress = employeeDto.HomeAddress,
                CurrentSalary = employeeDto.CurrentSalary,
                Role = employeeDto.Role
            };

            var createdEmployee = await _employeeRepository.AddNewEmployeeAsync(employeeToCreate);

            return CreatedAtAction(nameof(GetEmployeeAsync), new{ id = employeeToCreate.Id }, _mapper.Map<EmployeeDto>(createdEmployee));
        }

        // PUT api/employees/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto employeeDto)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeAsync(id);
            
            if (existingEmployee is null) return NotFound();
           
            var updatedEmployee = _mapper.Map(employeeDto, existingEmployee);

            await _employeeRepository.UpdateEmployeeAsync(updatedEmployee);

            return NoContent();
        }

        // PUT api/employees/salary
        [HttpPut("salary/{id}")]
        public async Task<ActionResult> UpdateEmployeeSalary(Guid id, int salary)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeAsync(id);
            
            if (existingEmployee is null) return NotFound();

            await _employeeRepository.UpdateEmployeeSalary(existingEmployee.Id, salary);

            return NoContent();
        }

        // DELETE api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid id)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeAsync(id);
            
            if (existingEmployee is null) return NotFound();

            await _employeeRepository.DeleteEmployee(id);

            return NoContent();
        }
    }
}
 