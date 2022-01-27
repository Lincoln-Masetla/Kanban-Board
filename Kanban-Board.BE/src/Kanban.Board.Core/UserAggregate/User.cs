using Kanban.Board.SharedKernel;
using Ardalis.GuardClauses;
using Kanban.Board.SharedKernel.Interfaces;

namespace Kanban.Board.Core.UserAggregate;

public class User : BaseEntity, IAggregateRoot
{
  public string Name { get; set; }
  public string Email { get; set; }

  public User(string name, string email)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name)); 
    Email = Guard.Against.NullOrEmpty(email, nameof(email)); 
  }
}

