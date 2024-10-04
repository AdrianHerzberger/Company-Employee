using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace Company_Employee.RepositoryContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseMySql(configuration.GetConnectionString("mysqlConnection"),
                    new MySqlServerVersion(new Version(8, 0, 21)),
                        b => b.MigrationsAssembly("CompanyEmployee"));

            return new RepositoryContext(builder.Options);
        }
    }
}
