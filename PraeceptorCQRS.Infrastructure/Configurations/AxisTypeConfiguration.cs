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
    }
}
