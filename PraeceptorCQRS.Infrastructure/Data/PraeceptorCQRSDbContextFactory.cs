using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PraeceptorCQRS.Infrastructure.Data
{
    public class PraeceptorCQRSDbContextFactory
        : IDesignTimeDbContextFactory<PraeceptorCQRSDbContext>
    {
        public PraeceptorCQRSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PraeceptorCQRSDbContext>();

            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-C50EKSV;" +
                "Initial Catalog=PrCQRS_DB;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=False;" +
                "TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False"
                );

            return new PraeceptorCQRSDbContext(optionsBuilder.Options);
        }
    }
}

