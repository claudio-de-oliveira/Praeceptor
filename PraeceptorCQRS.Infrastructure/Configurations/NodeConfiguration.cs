using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class NodeConfiguration : IEntityTypeConfiguration<Node>
    {
        public void Configure(EntityTypeBuilder<Node> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Nodes);

            modelBuilder
                .HasKey(x => x.Id);
        }
    }
}
