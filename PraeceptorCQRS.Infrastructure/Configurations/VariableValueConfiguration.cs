using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class VariableValueConfiguration : IEntityTypeConfiguration<VariableValue>
    {
        public void Configure(EntityTypeBuilder<VariableValue> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.VariableValues);

            modelBuilder
                .HasKey(x => x.Id);
        }
    }
}
