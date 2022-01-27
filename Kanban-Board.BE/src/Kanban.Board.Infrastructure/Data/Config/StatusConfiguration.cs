using Kanban.Board.Core.StatusAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.Board.Infrastructure.Data.Config;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
  public void Configure(EntityTypeBuilder<Status> builder)
  {
    builder.Property(t => t.Name)
        .IsRequired();
  }
}

