using meu_projeto.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace meu_projeto.Web.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        
    }
}
