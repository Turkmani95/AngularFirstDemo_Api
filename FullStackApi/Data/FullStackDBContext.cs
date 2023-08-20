using FullStackApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Data
{
    public class FullStackDBContext : DbContext
    {
        public FullStackDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
