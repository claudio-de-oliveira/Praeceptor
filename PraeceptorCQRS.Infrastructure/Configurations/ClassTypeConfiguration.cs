using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class ClassTypeConfiguration : IEntityTypeConfiguration<ClassType>
    {
        public void Configure(EntityTypeBuilder<ClassType> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.ClassTypes);

            modelBuilder
                .HasKey(x => x.Id);
        }
    }
}
