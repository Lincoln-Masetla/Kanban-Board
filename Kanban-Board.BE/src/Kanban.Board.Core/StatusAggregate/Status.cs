using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Kanban.Board.SharedKernel;
using Kanban.Board.SharedKernel.Interfaces;

namespace Kanban.Board.Core.StatusAggregate;

public class Status : BaseEntity, IAggregateRoot
{
  public string Name { get; set; }

  public Status(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }
}
