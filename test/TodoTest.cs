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
    public void ShouldCreateBasicTodo()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var todo = new Todo() { Status = StatusEnum.Open, Priority = PriorityEnum.Low, Title = "Code", Due = new(2020, 10, 3) };

      ctx.Add(todo);
      ctx.SaveChanges();

      ctx.Todos.Count().Should().Be(1);
    }

    [Fact]
    public void TodoNavigationLinksShouldWork()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var tag = new Tag() { Name = "Programming", Color = ColorEnum.Green };
      var comment = new Comment() { Text = "Test comment" };
      var todo = new Todo() { Status = StatusEnum.Open, Priority = PriorityEnum.Low, Title = "Code", Due = new(2020, 10, 3) };
      todo.Tags.Add(tag);
      todo.Comments.Add(comment);

      ctx.Add(todo);
      ctx.SaveChanges();

      ctx.Tags.Count().Should().Be(1);
      ctx.Comments.Count().Should().Be(1);
      ctx.Todos.Count().Should().Be(1);
      ctx.Tags.First().Todo.Title.Should().Be("Code");
      ctx.Comments.First().Todo.Priority.Should().Be(PriorityEnum.Low);
      ctx.Todos.First().Tags.First().Name.Should().Be("Programming");
      ctx.Todos.First().Comments.First().Text.Should().Be("Test comment");
    }
  }
}
