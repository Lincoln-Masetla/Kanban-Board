using Ardalis.Specification;

namespace Kanban.Board.Core.StatusAggregate.Specifications;

public class StatusByIdSpec : Specification<Status>, ISingleResultSpecification
{
  public StatusByIdSpec(int statusId)
  {
    Query.Where(status => status.Id == statusId);
  }
}
