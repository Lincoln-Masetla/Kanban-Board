using Ardalis.GuardClauses;
using Kanban.Board.SharedKernel;
using Kanban.Board.SharedKernel.Interfaces;

namespace Kanban.Board.Core.WorkItemAggregate;

public class WorkItem : BaseEntity, IAggregateRoot
{
  public string Title { get; set; }
  public string Description { get; set; }
  public int StatusId { get; set; }
  public int UserId { get; set; }

  public WorkItem(string title, string description, int userId, int statusId )
  {
    Title = Guard.Against.NullOrEmpty(title, nameof(title));
    Description = description;
    UserId = Guard.Against.Null(userId, nameof(userId));
    StatusId = Guard.Against.Null(statusId, nameof(statusId));
  }
}
