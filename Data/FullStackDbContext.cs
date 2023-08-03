using fullstackapi.Models;
using Microsoft.EntityFrameworkCore;

namespace fullstackapi.Data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Enployees { get; set; }
    }
}
