using FullStackWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackWebAPI.Data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {

        }

        //property
        public DbSet<Employee> Employees { get; set; }
    }
}
