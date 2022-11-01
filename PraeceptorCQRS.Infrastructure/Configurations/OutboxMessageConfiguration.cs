using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraeceptorCQRS.Infrastructure.Outbox;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations;

internal sealed class OutboxMessageConfiguration 
    : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> modelBuilder)
    {
        modelBuilder
            .ToTable(TableNames.OutboxMessages);

        modelBuilder
            .HasKey(x => x.Id);
    }
}
