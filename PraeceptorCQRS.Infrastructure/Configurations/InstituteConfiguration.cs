using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class InstituteConfiguration : IEntityTypeConfiguration<Institute>
    {
        public void Configure(EntityTypeBuilder<Institute> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Institutes);

            modelBuilder
                .HasKey(x => x.Id);
            modelBuilder
                .HasMany(d => d.Classes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Courses)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Preceptors)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.PreceptorDegreeTypes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Groups)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
