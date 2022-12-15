using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class VariableXConfiguration : IEntityTypeConfiguration<VariableX>
    {
        public void Configure(EntityTypeBuilder<VariableX> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.VariablesX);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .Property(x => x.GroupName)
                .IsRequired();

            modelBuilder
                .Property(x => x.VariableName)
                .IsRequired();
        }
    }
}
