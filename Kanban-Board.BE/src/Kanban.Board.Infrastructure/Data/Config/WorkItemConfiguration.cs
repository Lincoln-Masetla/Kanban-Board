using Kanban.Board.Core.WorkItemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.Board.Infrastructure.Data.Config;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
{
  public void Configure(EntityTypeBuilder<WorkItem> builder)
  {
    builder.Property(t => t.Title)
        .IsRequired();
    builder.Property(t => t.StatusId)
      .IsRequired();
    builder.Property(t => t.UserId)
        .IsRequired();
  }
}

