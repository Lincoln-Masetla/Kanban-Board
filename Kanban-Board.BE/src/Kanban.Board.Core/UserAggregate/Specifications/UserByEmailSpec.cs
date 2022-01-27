
using Ardalis.Specification;

namespace Kanban.Board.Core.UserAggregate.Specifications;

public class UserByEmailSpec : Specification<User>, ISingleResultSpecification
{
  public UserByEmailSpec(string email)
  {
    Query.Where(user => user.Email == email);
  }
}
