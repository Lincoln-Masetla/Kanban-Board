using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Kanban.Board.Core.StatusAggregate.Specifications;

public class StatusByNameSpec : Specification<Status>, ISingleResultSpecification
{
  public StatusByNameSpec(string statusName)
  {
    Query.Where(status => status.Name == statusName);
  }
}
