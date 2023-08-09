using EmployeeCRUD.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeCRUD.DAL.Entities;
using EmployeeCRUD.BAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EmployeeCRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeCRUDController : ControllerBase
    {
        private readonly ILogger<EmployeeCRUDController> logger;
        private readonly EmployeeDBContext dbContext;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeCRUDController(ILogger<EmployeeCRUDController> logger, EmployeeDBContext dbContext, IEmployeeRepository employeeRepository)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.employeeRepository = employeeRepository;
        }

        // It's a HttpGet api end point to get the list of employees
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await dbContext.Employees.ToListAsync());
        }   //GetAllEmployees

        // It's a HttpGet api end point to get the list of employees
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            var FoundEmployee = dbContext.Employees.SingleOrDefault(x => x.Firstname == employee.Firstname && x.Lastname == employee.Lastname && x.Email == employee.Email);

            if (FoundEmployee == null)
            {
                var NewEmployee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Firstname = employee.Firstname,
                    Lastname = employee.Lastname,
                    Email = employee.Email,
                    Age = employee.Age,
                };
                await dbContext.Employees.AddAsync(NewEmployee);
                await dbContext.SaveChangesAsync();
                return Ok(NewEmployee);
            }
            return Ok("The combination of first name, last name and email address should be unique.");
        }   //CreateEmployee

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid Id, EmployeeDTO employeeUpdateDto)
        {
            var FoundEmployee = await dbContext.Employees.FindAsync(Id);
            if (FoundEmployee.Firstname == employeeUpdateDto.Firstname && FoundEmployee.Lastname == employeeUpdateDto.Lastname && FoundEmployee.Email == employeeUpdateDto.Email)
            {
                return Ok("The combination of first name, last name and email address should be unique.");
            }
            else if (FoundEmployee != null)
            {
                FoundEmployee.Firstname = employeeUpdateDto.Firstname;
                FoundEmployee.Lastname = employeeUpdateDto.Lastname;
                FoundEmployee.Email = employeeUpdateDto.Email;
                FoundEmployee.Age = employeeUpdateDto.Age;
                dbContext.Employees.Update(FoundEmployee);
                await dbContext.SaveChangesAsync();
                return Ok(employeeUpdateDto);
            }
            return NotFound();
        }   //UpdateEmployee

        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid Id)
        {
            var Employee = await dbContext.Employees.FindAsync(Id);
            if (Employee != null)
            {
                dbContext.Remove(Employee);
                await dbContext.SaveChangesAsync();
                return Ok(Employee);
            }
            return NotFound();
        }   //DeleteEmployee

        // It's a HttpGet api end point to get the list of employees by their first or last name
        [HttpGet]
        [Route("Search/{FirstOrLastName}")]
        public async Task<IActionResult> GetEmployeeByName([FromRoute] string FirstOrLastName)
        {
            var query = dbContext.Employees.AsQueryable();
            var FoundEmployees = query.Where(x => x.Firstname == FirstOrLastName || x.Lastname == FirstOrLastName).ToList();

            if (FoundEmployees == null)
            {
                return NotFound();
            }
            return Ok(FoundEmployees);
        }   //GetEmployeeByName
    }
}
