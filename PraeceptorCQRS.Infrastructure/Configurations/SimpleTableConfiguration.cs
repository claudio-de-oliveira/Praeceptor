using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class SimpleTableConfiguration : IEntityTypeConfiguration<SimpleTable>
    {
        public void Configure(EntityTypeBuilder<SimpleTable> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.SimpleTablesTable);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(d => d.Institute)
                .WithMany(p => p.SimpleTables);
        }
    }
}