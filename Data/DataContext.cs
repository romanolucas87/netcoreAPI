using Microsoft.EntityFrameworkCore;
using netcoreAPI.Models;

namespace netcoreAPI.Controllers.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions <DataContext> options) : base(options){}

        public DbSet<Value> Values { get; set; }
    }
}