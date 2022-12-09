using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Classes);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(d => d.Institute)
                .WithMany(p => p.Classes);
            modelBuilder
                .HasMany(d => d.Components)
                .WithOne(p => p.Class);
            modelBuilder
                .HasOne(d => d.Type)
                .WithMany(p => p.Classes);
            modelBuilder
                .HasMany(d => d.Peas)
                .WithOne(p => p.Class);
        }
    }
}