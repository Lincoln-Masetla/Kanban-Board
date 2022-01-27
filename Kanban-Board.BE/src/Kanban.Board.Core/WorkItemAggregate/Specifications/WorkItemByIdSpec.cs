
using Ardalis.Specification;

namespace Kanban.Board.Core.WorkItemAggregate.Specifications;

public class WorkItemByIdSpec : Specification<WorkItem>, ISingleResultSpecification
{
  public WorkItemByIdSpec(int workItemId)
  {
    Query
        .Where(workItem => workItem.Id == workItemId);
  }
}
