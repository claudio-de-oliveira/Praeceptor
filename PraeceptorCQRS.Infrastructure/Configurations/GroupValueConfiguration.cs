using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class GroupValueConfiguration : IEntityTypeConfiguration<GroupValue>
    {
        public void Configure(EntityTypeBuilder<GroupValue> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.GroupValues);

            modelBuilder
                .HasKey(x => x.Id);
        }
    }
}
