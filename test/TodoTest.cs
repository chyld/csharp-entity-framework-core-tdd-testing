using System;
using Xunit;
using FluentAssertions;
using lib;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace test
{
  public class TodoTest
  {
    private DbContextOptions<Database> _options;

    public TodoTest()
    {
      var conn = new SqliteConnection("DataSource=:memory:");
      conn.Open();

      _options = new DbContextOptionsBuilder<Database>()
         .UseSqlite(conn)
         .Options;
    }

    [Fact]
    public void Test1()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var tag = new Tag() { Name = "Programming", Color = ColorEnum.Green };
      var todo = new Todo() { Status = StatusEnum.Open, Priority = PriorityEnum.Low, Title = "Code", Due = new(2020, 10, 3) };
      todo.Tags.Add(tag);

      ctx.Add(todo);
      ctx.SaveChanges();

      ctx.Tags.Count().Should().Be(1);
      ctx.Todos.Count().Should().Be(1);
      ctx.Tags.First().Todo.Title.Should().Be("Code");
      ctx.Todos.First().Tags.First().Name.Should().Be("Programming");
    }
  }
}
