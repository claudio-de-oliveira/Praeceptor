using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class SubSectionConfiguration : IEntityTypeConfiguration<SubSection>
    {
        public void Configure(EntityTypeBuilder<SubSection> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.SubSections);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(p => p.Institute)
                .WithMany(d => d.SubSections);
        }
    }
}
