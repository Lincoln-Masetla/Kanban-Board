using Ardalis.Specification;

namespace Kanban.Board.Core.WorkItemAggregate.Specifications;

public class WorkItemByStatusSpec : Specification<WorkItem>, ISingleResultSpecification
{
  public WorkItemByStatusSpec(int statusId)
  {
    Query
        .Where(workItem => workItem.StatusId == statusId);
  }
}
