using Microsoft.EntityFrameworkCore;

namespace PraeceptorCQRS.Infrastructure.Data;

public class PraeceptorCQRSDbContext : DbContext
{
    public PraeceptorCQRSDbContext(DbContextOptions<PraeceptorCQRSDbContext> options)
        : base(options)
    { /* Nothing more todo */ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    public int ExecuteSqlRaw(string sql, params object[] parameters)
    {
        int rows = 0;
        var transaction = Database.BeginTransaction(System.Data.IsolationLevel.Serializable);

        try
        {
            rows = Database.ExecuteSqlRaw(sql, parameters);
            // Commit transaction if all commands succeed, transaction will auto-rollback
            // when disposed if either commands fails
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return rows;
    }
}

