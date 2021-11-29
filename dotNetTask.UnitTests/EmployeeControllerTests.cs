using System;
using System.Threading.Tasks;
using AutoMapper;
using dotNetTask.API;
using dotNetTask.API.Controllers;
using dotNetTask.API.Dtos;
using dotNetTask.API.Entities;
using dotNetTask.API.Helpers;
using dotNetTask.API.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using LoggerService;

namespace dotNetTask.UnitTests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeRepository> repositoryStub  = new();
        private readonly Mock<ILoggerManager> loggerStub = new();
        private readonly MapperConfiguration mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
        private readonly Random rand = new();
        [Fact]
        public async Task GetEmployeeAsync_WithUnexistingEmployee_ReturnsNorFound()   //UnitOfWork_SateUnderTest_ExpectedBehavior
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Employee)null);

            var controller = new EmployeesController(repositoryStub.Object, mockMapper.CreateMapper(), loggerStub.Object);
            
            //Act
            var result = await controller.GetEmployeeAsync(Guid.NewGuid());
            
            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetEmployeeAsync_WithExistingEmployee_ReturnsExpectedEmployee() 
        {
            // Arrange
            var expectedEmployee = CreateRandomEmployee();
            repositoryStub.Setup(repo => repo.GetEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedEmployee);

            var controller = new EmployeesController(repositoryStub.Object, mockMapper.CreateMapper(), loggerStub.Object);

            //Act
            var actionResult = await controller.GetEmployeeAsync(Guid.NewGuid());
            var result = actionResult.Result as OkObjectResult;
            
            //Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo (
                expectedEmployee,
                options => options.ExcludingMissingMembers());
        }
        [Fact]
        public async Task GetEmployeesAsync_WithExistingEmployees_ReturnsAllItems() 
        {
            // Arrange
            var expectedEmployees = new[]{CreateRandomEmployee(), CreateRandomEmployee(), CreateRandomEmployee()};
            repositoryStub.Setup(repo => repo.GetEmployeesAsync())
                .ReturnsAsync(expectedEmployees);

            var controller = new EmployeesController(repositoryStub.Object, mockMapper.CreateMapper(), loggerStub.Object);
            
            //Act
            var actionResult = await controller.GetEmployeesAsync();
            var result = actionResult.Result as OkObjectResult;
            
            //Assert
            Assert.IsType<OkObjectResult>(result);
            result.Value.Should().BeEquivalentTo(
                expectedEmployees,
                options => options.ExcludingMissingMembers());
        }
        [Fact]
        public async Task AddEmployeeAsync_WithEmployeeToCreate_ReturnsCreatedEmployee() 
        {
            // Arrange
            var employeeToCreate = new CreateEmployeeDto()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                BirtDate = DateTime.Parse("1990/01/01"),
                EmploymentDate = DateTime.Parse("2021/01/01"),
                BossId = Guid.NewGuid(),
                HomeAddress = Guid.NewGuid().ToString(),
                CurrentSalary = rand.Next(20000),
                Role = EmployeeRoles.Other,
            };
            
            var controller = new EmployeesController(repositoryStub.Object, mockMapper.CreateMapper(), loggerStub.Object);
            
            //Act
            var actionResult = await controller.AddEmployeeAsync(employeeToCreate);
            
            //Assert
            var createdEmployee = (actionResult.Result as CreatedAtActionResult).Value as EmployeeDto;
            employeeToCreate.Should().BeEquivalentTo(
                createdEmployee,
                options => options.ComparingByMembers<EmployeeDto>().ExcludingMissingMembers());
            createdEmployee.Id.Should().NotBeEmpty();
        }
        [Fact]
        public async Task UpdateEmployeeAsync_WithExistingEmployees_ReturnsNoContent() 
        {
            // Arrange
            var existingEmployee = CreateRandomEmployee();
            repositoryStub.Setup(repo => repo.GetEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingEmployee);

            var employeeToUpdate = new UpdateEmployeeDto()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                BirtDate = DateTime.Parse("1995/01/01"),
                EmploymentDate = DateTime.UtcNow,
                BossId = Guid.NewGuid(),
                HomeAddress = Guid.NewGuid().ToString(),
                CurrentSalary = rand.Next(20000),
                Role = EmployeeRoles.Receptionist
            };
            var controller = new EmployeesController(repositoryStub.Object, mockMapper.CreateMapper(), loggerStub.Object);
            
            //Act
            var actionResult = await controller.UpdateEmployeeAsync(existingEmployee.Id, employeeToUpdate);
            
            //Assert
            actionResult.Should().BeOfType<NoContentResult>();
        }
        [Fact]
        public async Task UpdateEmployeeAsync_WithUnexistingEmployees_ReturnsNotFound() 
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Employee)null);

            var employeeToUpdate = new UpdateEmployeeDto()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                BirtDate = DateTime.Parse("1995/01/01"),
                EmploymentDate = DateTime.UtcNow,
                BossId = Guid.NewGuid(),
                HomeAddress = Guid.NewGuid().ToString(),
                CurrentSalary = rand.Next(20000),
                Role = EmployeeRoles.Programmer
            };
            var controller = new EmployeesController(repositoryStub.Object, mockMapper.CreateMapper(), loggerStub.Object);
            
            //Act
            var actionResult = await controller.UpdateEmployeeAsync(Guid.NewGuid(), employeeToUpdate);
            
            //Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task DeleteEmployeeAsync_WithExistingEmployees_ReturnsNoContent() 
        {
            // Arrange
            var existingEmployee = CreateRandomEmployee();
            repositoryStub.Setup(repo => repo.GetEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingEmployee);

            var controller = new EmployeesController(repositoryStub.Object, mockMapper.CreateMapper(), loggerStub.Object);
            
            //Act
            var actionResult = await controller.DeleteEmployeeAsync(existingEmployee.Id);
            
            //Assert
            actionResult.Should().BeOfType<NoContentResult>();
        }
        [Fact]
        public async Task DeleteEmployeeAsync_WithUnexistingEmployees_ReturnsNotFound() 
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Employee)null);

            var controller = new EmployeesController(repositoryStub.Object, mockMapper.CreateMapper(), loggerStub.Object);
            
            //Act
            var actionResult = await controller.DeleteEmployeeAsync(Guid.NewGuid());
            
            //Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }
        private Employee CreateRandomEmployee()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                BirtDate = DateTime.Parse("1990/01/01"),
                EmploymentDate = DateTime.Now,
                Boss = null,
                HomeAddress = Guid.NewGuid().ToString(),
                CurrentSalary = rand.Next(20000),
                Role = EmployeeRoles.HRSpecialist
            };
        }
    }
}
