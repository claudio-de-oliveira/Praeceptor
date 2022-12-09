using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PraeceptorCQRS.Infrastructure.Constants;

using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Infrastructure.Configurations;

internal sealed class CourseSocialBodyEntryConfiguration
    : IEntityTypeConfiguration<SocialBodyEntry>
{
    public void Configure(EntityTypeBuilder<SocialBodyEntry> modelBuilder)
    {
        modelBuilder
            .ToTable(TableNames.CourseSocialBodyEntries);

        modelBuilder
            .HasKey(x => new { x.CourseId, x.PreceptorId, x.RoleId });

        modelBuilder
            .HasOne(d => d.Course)
            .WithMany(p => p.CourseSocialBodyEntries);
        modelBuilder
            .HasOne(d => d.Preceptor)
            .WithMany(p => p.CourseSocialBodyEntries);
        modelBuilder
            .HasOne(d => d.Role)
            .WithMany(p => p.CourseSocialBodyEntries);
    }
}
