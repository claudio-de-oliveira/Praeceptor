using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Sections);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(p => p.Institute)
                .WithMany(d => d.Sections);
        }
    }
}
