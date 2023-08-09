using EmployeeCRUD.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace EmployeeCRUD.DAL
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
