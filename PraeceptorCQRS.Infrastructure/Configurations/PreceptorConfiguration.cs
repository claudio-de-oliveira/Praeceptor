using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class PreceptorConfiguration : IEntityTypeConfiguration<Preceptor>
    {
        public void Configure(EntityTypeBuilder<Preceptor> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Preceptors);

            modelBuilder
                .HasKey(x => x.Id);
        }
    }
}
