using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class PeaConfiguration : IEntityTypeConfiguration<Pea>
    {
        public void Configure(EntityTypeBuilder<Pea> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Peas);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(d => d.Class)
                .WithMany(p => p.Peas);
        }
    }
}