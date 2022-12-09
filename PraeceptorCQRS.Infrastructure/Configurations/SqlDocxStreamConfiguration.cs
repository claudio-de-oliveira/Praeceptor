using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class SqlDocxStreamConfiguration : IEntityTypeConfiguration<SqlDocxStream>
    {
        public void Configure(EntityTypeBuilder<SqlDocxStream> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.SqlDocxStreams);

            modelBuilder
                .HasKey(x => x.Id);
        }
    }
}