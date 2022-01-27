namespace Kanban.Board.Web.ApiModels;

public class UserDTO
{
  public UserDTO(int id, string name, string email)
  {
    Id = id;
    Name = name;
    Email = email;
  }
  public int Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
}

public class CreateUserDTO
{
  public CreateUserDTO(string name, string email)
  {
    Name = name;
    Email = email;
  }
  public string Name { get; set; }
  public string Email { get; set; }
}
