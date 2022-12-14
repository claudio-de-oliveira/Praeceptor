using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class PreceptorRegimeTypeConfiguration : IEntityTypeConfiguration<PreceptorRegimeType>
    {
        public void Configure(EntityTypeBuilder<PreceptorRegimeType> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.PreceptorRegimeTypes);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(d => d.Institute)
                .WithMany(p => p.PreceptorRegimeTypes);
            modelBuilder
                .HasMany(d => d.Preceptors)
                .WithOne(p => p.RegimeType);
        }
    }
}
