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
                .HasOne(d => d.Holding)
                .WithMany(p => p.Institutes)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Documents)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Chapters)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Sections)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.SubSections)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.SubSubSections)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Classes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.ClassTypes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Courses)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.Preceptors)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.PreceptorDegreeTypes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.PreceptorRegimeTypes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.PreceptorRoleTypes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.AxisTypes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .HasMany(d => d.AxisTypes)
                .WithOne(p => p.Institute)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}