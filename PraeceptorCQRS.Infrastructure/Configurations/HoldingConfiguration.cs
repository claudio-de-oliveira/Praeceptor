using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class HoldingConfiguration : IEntityTypeConfiguration<Holding>
    {
        public void Configure(EntityTypeBuilder<Holding> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Holdings);

            modelBuilder
                .HasKey(x => x.Id);
        }
    }
}
