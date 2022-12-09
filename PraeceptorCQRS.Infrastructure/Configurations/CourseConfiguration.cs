using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Courses);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(d => d.Institute)
                .WithMany(p => p.Courses);
            modelBuilder
                .HasMany(d => d.CourseSocialBodyEntries)
                .WithOne(p => p.Course);
            modelBuilder
                .HasMany(d => d.Components)
                .WithOne(p => p.Course);
        }
    }
}
