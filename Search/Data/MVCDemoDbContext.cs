using Microsoft.EntityFrameworkCore;
using Search.Models.Domain;

namespace Search.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; } // table by name of Employees will be created based on the properties defined in Search.Models.Domain- Employee.cs


    }
}
