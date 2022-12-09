using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations;

internal sealed class AxisTypeConfiguration 
    : IEntityTypeConfiguration<AxisType>
{
    public void Configure(EntityTypeBuilder<AxisType> modelBuilder)
    {
        modelBuilder
            .ToTable(TableNames.AxisTypes);

        modelBuilder
            .HasKey(x => x.Id);

        modelBuilder
            .HasOne(d => d.Institute)
            .WithMany(p => p.AxisTypes);
        modelBuilder
            .HasMany(d => d.Components)
            .WithOne(p => p.Axis);
    }
}
