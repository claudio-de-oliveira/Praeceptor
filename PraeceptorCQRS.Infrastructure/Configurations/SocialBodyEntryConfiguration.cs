using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations;

internal sealed class SocialBodyEntryConfiguration : IEntityTypeConfiguration<SocialBodyEntry>
{
    public void Configure(EntityTypeBuilder<SocialBodyEntry> modelBuilder)
    {
        modelBuilder
            .ToTable(TableNames.CourseSocialBodyEntries);

        modelBuilder
            .HasKey(x => new { x.CourseId, x.PreceptorId, x.RoleId } );

        modelBuilder
            .HasOne(p => p.Course)
            .WithMany(d => d.CourseSocialBodyEntries);
        modelBuilder
            .HasOne(p => p.Preceptor)
            .WithMany(d => d.CourseSocialBodyEntries);
        modelBuilder
            .HasOne(p => p.Role)
            .WithMany(d => d.CourseSocialBodyEntries);
    }
}
