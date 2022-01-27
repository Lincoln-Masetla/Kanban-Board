using Kanban.Board.Core.StatusAggregate;
using Kanban.Board.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Board.Web;

public static class SeedData
{
  public static readonly Status  todo = new Status("todo");
  public static readonly Status inprogress = new Status("inprogress");
  public static readonly Status done = new Status("done");

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      // Look for any TODO items.
      if (dbContext.Statuses.Any())
      {
        return;   // DB has been seeded
      }

      PopulateTestData(dbContext);


    }
  }

  private static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Statuses)
    {
      dbContext.Remove(item);
    }

    dbContext.Statuses.Add(todo);
    dbContext.Statuses.Add(inprogress);
    dbContext.Statuses.Add(done);

    dbContext.SaveChanges();
  }
}
