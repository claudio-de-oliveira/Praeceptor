using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class VariableConfiguration : IEntityTypeConfiguration<Variable>
    {
        public void Configure(EntityTypeBuilder<Variable> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Variables);

            modelBuilder
                .HasKey(x => x.Id);
            modelBuilder
                .HasMany(d => d.Values)
                .WithOne(p => p.Variable)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
