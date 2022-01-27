using Kanban.Board.Core.UserAggregate;
using Kanban.Board.Core.UserAggregate.Specifications;
using Kanban.Board.SharedKernel.Interfaces;
using Kanban.Board.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Board.Web.Api;
public class UsersController : BaseApiController
{
  private readonly IRepository<User> _repository;

  public UsersController(IRepository<User> repository)
  {
    _repository = repository;
  }

  [HttpGet]
  public async Task<IActionResult> List()
  {
    var userDTOs = (await _repository.ListAsync())
        .Select(user => new UserDTO
        (
            id: user.Id,
            name: user.Name,
            email: user.Email
        ))
        .ToList();

    return Ok(userDTOs);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var userSpec = new UserByIdSpec(id);
    var user = await _repository.GetBySpecAsync(userSpec);
    if (user == null)
    {
      return NotFound();
    }

    var result = new UserDTO
    (
        id: user.Id,
        name: user.Name,
        email: user.Email
    );

    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] CreateUserDTO request)
  {
    var newUser = new User(request.Name, request.Email);

    var userSpec = new UserByEmailSpec(request.Email);
    var user = await _repository.GetBySpecAsync(userSpec);

    if (user != null) return BadRequest("Email alredy Exists");

    var createdUser = await _repository.AddAsync(newUser);

    var result = new UserDTO
    (
        id: createdUser.Id,
        name: createdUser.Name,
        email: createdUser.Email
    );
    return Ok(result);
  }

  [HttpPut]
  public async Task<IActionResult> Update([FromBody] UserDTO request)
  {
    var UserSpec = new UserByIdSpec(request.Id);
    var user = await _repository.GetBySpecAsync(UserSpec);
    if (user == null) return NotFound("No such User");
    user.Name = request.Name;
    user.Email = request.Email;
    await _repository.UpdateAsync(user);

    return Ok(user);
  }
}
