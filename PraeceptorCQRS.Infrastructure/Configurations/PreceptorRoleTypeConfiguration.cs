using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PraeceptorCQRS.Infrastructure.Constants;

using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Infrastructure.Configurations;

internal sealed class PreceptorRoleTypeConfiguration
    : IEntityTypeConfiguration<PreceptorRoleType>
{
    public void Configure(EntityTypeBuilder<PreceptorRoleType> modelBuilder)
    {
        modelBuilder
            .ToTable(TableNames.PreceptorRoleTypes);

        modelBuilder
            .HasKey(x => x.Id);

        modelBuilder
            .HasOne(d => d.Institute)
            .WithMany(p => p.PreceptorRoleTypes);
        modelBuilder
            .HasMany(d => d.CourseSocialBodyEntries)
            .WithOne(p => p.Role);
    }
}