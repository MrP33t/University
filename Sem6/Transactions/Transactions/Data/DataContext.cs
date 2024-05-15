using Microsoft.EntityFrameworkCore;
using Transactions.Models;

namespace Transactions.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Advertisment> Advertisments { get; set; }
    }
}
