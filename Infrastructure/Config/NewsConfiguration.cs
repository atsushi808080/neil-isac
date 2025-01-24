using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
       builder.Property(x => x.Sort).HasColumnType("decimal(18,2)");
       builder.Property(x=>x.Title).IsRequired();
    }
}
