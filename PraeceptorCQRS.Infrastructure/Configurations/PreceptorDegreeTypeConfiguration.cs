using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class PreceptorDegreeTypeConfiguration : IEntityTypeConfiguration<PreceptorDegreeType>
    {
        public void Configure(EntityTypeBuilder<PreceptorDegreeType> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.PreceptorDegreeTypes);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(d => d.Institute)
                .WithMany(p => p.PreceptorDegreeTypes);
            modelBuilder
                .HasMany(d => d.Preceptors)
                .WithOne(p => p.DegreeType);
        }
    }
}
