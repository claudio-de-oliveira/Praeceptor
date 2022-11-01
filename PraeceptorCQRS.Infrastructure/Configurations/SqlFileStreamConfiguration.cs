using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class SqlFileStreamConfiguration : IEntityTypeConfiguration<SqlFileStream>
    {
        public void Configure(EntityTypeBuilder<SqlFileStream> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.SqlFileStreams);

            modelBuilder
                .HasKey(x => x.Id);
        }
    }
}
