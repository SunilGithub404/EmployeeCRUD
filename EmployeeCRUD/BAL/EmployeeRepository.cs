using EmployeeCRUD.BAL.Interfaces;
using EmployeeCRUD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EmployeeCRUD.DAL.Entities;

namespace EmployeeCRUD.BAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger<EmployeeRepository> logger;
        private readonly EmployeeDBContext dbContext;

        public EmployeeRepository(ILogger<EmployeeRepository> logger, EmployeeDBContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.AddSomeEmployeesInMemoryAsync();
        }

        // It is method that will add some employees into in memory databasse
        public void AddSomeEmployeesInMemoryAsync()
        {
            if (this.dbContext.Employees.Count() == 0)
            {
                var EmployeesList = new List<Employee>
                {
                new Employee
                {
                    Id= new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Firstname ="Joydip",
                    Lastname ="Kanjilal",
                    Email= "Joydip@Joydip.com",
                    Age = 23
                },
                new Employee
                {
                    Id= Guid.NewGuid(),
                    Firstname ="Yashavanth",
                    Lastname ="Kanetkar",
                    Email= "Kanetkar@Kanetkar.com",
                    Age = 34
                },
                new Employee
                {
                    Id= Guid.NewGuid(),
                    Firstname ="Bhumika",
                    Lastname ="Ansari",
                    Email= "Ansari@Ansari.com",
                    Age = 12
                },
                new Employee
                {
                    Id= Guid.NewGuid(),
                    Firstname ="Bhumika",
                    Lastname ="Saini",
                    Email= "Saini@Saini.com",
                    Age = 45
                }
                };
                this.dbContext.Employees.AddRangeAsync(EmployeesList);
                this.dbContext.SaveChangesAsync();
            }
        }   //AddSomeEmployeesInMemoryAsync

        public void AddMockEmployeesInMemoryAsync()
        {
            var EmployeesList = new List<Employee>
                {
                new Employee
                {
                    Id= Guid.NewGuid(),
                    Firstname ="Joydip",
                    Lastname ="Kanjilal",
                    Email= "Joydip@Joydip.com",
                    Age = 23
                },
                new Employee
                {
                    Id= Guid.NewGuid(),
                    Firstname ="Yashavanth",
                    Lastname ="Kanetkar",
                    Email= "Kanetkar@Kanetkar.com",
                    Age = 34
                },
                new Employee
                {
                    Id= Guid.NewGuid(),
                    Firstname ="Bhumika",
                    Lastname ="Ansari",
                    Email= "Ansari@Ansari.com",
                    Age = 12
                },
                new Employee
                {
                    Id= Guid.NewGuid(),
                    Firstname ="Bhumika",
                    Lastname ="Saini",
                    Email= "Saini@Saini.com",
                    Age = 45
                }
                };
            this.dbContext.Employees.AddRangeAsync(EmployeesList);
            this.dbContext.SaveChangesAsync();
        }   //AddMockEmployeesInMemoryAsync
    }
}
