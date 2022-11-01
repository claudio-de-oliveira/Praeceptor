﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Constants;

namespace PraeceptorCQRS.Infrastructure.Configurations
{
    internal sealed class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> modelBuilder)
        {
            modelBuilder
                .ToTable(TableNames.Components);

            modelBuilder
                .HasKey(d => new { d.CourseId, d.Curriculum, d.Season, d.ClassId });
        }
    }
}
