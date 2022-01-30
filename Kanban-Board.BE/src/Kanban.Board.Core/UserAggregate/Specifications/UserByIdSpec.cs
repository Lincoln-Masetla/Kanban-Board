﻿
using Ardalis.Specification;

namespace Kanban.Board.Core.UserAggregate.Specifications;

public class UserByIdSpec : Specification<User>, ISingleResultSpecification
{
  public UserByIdSpec(int userId)
  {
    Query.Where(user => user.Id == userId);
  }
}
