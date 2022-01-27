using Kanban.Board.Core.StatusAggregate;
using Kanban.Board.Core.UserAggregate;

namespace Kanban.Board.Web.ApiModels;

public class WorkItemDTO
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public Task<User?> User { get; set; }
  public Task<Status?> Status { get; set; }

  public WorkItemDTO(int id, string title, string description, Task<User?> user, Task<Status?> status)
  {
    Id = id;
    Title = title;
    Description = description;
    User = user;
    Status = status;
  }
}

public class CreateWorkItemDTO
{
  public string Title { get; set; }
  public string Description { get; set; }
  public int UserId { get; set; }
  public int StatusId { get; set; }

  public CreateWorkItemDTO(string title, string description, int userId, int statusId)
  {
    Title = title;
    Description = description;
    UserId = userId;
    StatusId = statusId;
  }
}
