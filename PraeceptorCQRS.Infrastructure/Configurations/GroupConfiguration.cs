using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Groups);

            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .HasOne(p => p.Institute)
                .WithMany(d => d.Groups);
            modelBuilder
                .HasMany(p => p.Values)
                .WithOne(d => d.Group);
            modelBuilder
                .HasMany(p => p.Variables)
                .WithOne(d => d.Group);
        }
    }
}
