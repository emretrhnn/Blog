using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Context
{
    public class DbFactory : IDesignTimeDbContextFactory<DContext>
    {
        public DContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DContext>();
            optionsBuilder.UseSqlServer("Server =.\\SQLEXPRESS; Database = DinosaursDb; trusted_connection = true; multipleactiveresultsets = true; trustservercertificate = true; ");

            return new DContext(optionsBuilder.Options);
        }
    }
}
