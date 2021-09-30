using System;
using Xunit;
using FluentAssertions;
using lib;

namespace test
{
  public class UnitTest1
  {
    [Fact]
    public void Test1()
    {
      //   var options = SqliteInMemory.CreateOptions<Database>();
      // using var context = new Database(options);
      // context.Database.EnsureCreated();
      using var db = new Database();
      db.Add(new Tag() { Name = "Home", Color = ColorEnum.Blue });
      db.SaveChanges();
    }
  }
}
