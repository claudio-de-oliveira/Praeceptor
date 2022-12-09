using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class SubSubSectionConfiguration : IEntityTypeConfiguration<SubSubSection>
    {
        public void Configure(EntityTypeBuilder<SubSubSection> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.SubSubSections);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(p => p.Institute)
                .WithMany(d => d.SubSubSections);
        }
    }
}
