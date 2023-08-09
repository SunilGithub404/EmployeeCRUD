using EmployeeCRUD.BAL.Interfaces;
using EmployeeCRUD.Controllers;
using EmployeeCRUD.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using EmployeeCRUD.BAL;
using EmployeeCRUD.DAL.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUDTest
{
    public class EmployeeCRUDUnitTest
    {
        private readonly ILogger<EmployeeRepository> loggerEmp;
        private readonly EmployeeCRUDController EmpController;
        private readonly ILogger<EmployeeCRUDController> logger;
        private readonly EmployeeDBContext dbContext;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeCRUDUnitTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<EmployeeDBContext>().UseInMemoryDatabase("EmployeesDb").Options;
            dbContext = new EmployeeDBContext(dbContextOptions);
            employeeRepository = new EmployeeRepository(loggerEmp, dbContext);
            EmpController = new EmployeeCRUDController(logger, dbContext, employeeRepository);
        }

        [Fact]
        public async void Check_AllEmployee_Data_Return_OkResult()
        {
            // Act
            var okResult = await EmpController.GetAllEmployees() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Employee>>(okResult.Value);
            Assert.True(items.Count > 0);
        }

        [Fact]
        public async void GetByFirstOrLastName_ExistingNamePassed_ReturnsOkResult()
        {
            // Act
            var okResult = await EmpController.GetEmployeeByName("Bhumika") as OkObjectResult;
            // Assert
            Assert.IsType<List<Employee>>(okResult.Value);
            //Assert.Equal(2, items.Count);
        }

        [Fact]
        public async void Add_ValidEmployeePassed_ReturnsCreatedResponse()
        {
            // Arrange
            Employee testItem = new Employee
            {
                Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c223"),
                Firstname = "Ramesh",
                Lastname = "Ramesh",
                Email = "Ramesh@Kanetkar.com",
                Age = 34
            };
            // Act
            var createdResponse = await EmpController.CreateEmployee(testItem) as OkObjectResult;
            // Assert
            Assert.IsType<Employee>(createdResponse.Value);
        }

        [Fact]
        public async void Remove_ExistingEmployeePassed_ReturnsDeletedEmployee()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var createdResponse = await EmpController.DeleteEmployee(existingGuid) as OkObjectResult;
            // Assert
            Assert.IsType<Employee>(createdResponse.Value);
        }
    }
}
