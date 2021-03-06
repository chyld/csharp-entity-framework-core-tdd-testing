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

      var todo = new Todo() { Title = "Code", Due = new(2020, 10, 3) };

      ctx.Add(todo);
      ctx.SaveChanges();

      ctx.Todos.Count().Should().Be(1);
    }

    [Fact]
    public void ShouldAddComment()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var todo = new Todo() { Title = "Code", Due = new(2020, 10, 3) };
      todo.AddComment("this is a comment");

      ctx.Add(todo);
      ctx.SaveChanges();

      ctx.Todos.Count().Should().Be(1);
      ctx.Comments.Count().Should().Be(1);
      ctx.Comments.First().Text.Should().Be("this is a comment");
    }

    [Fact]
    public void TodoCommentNavigationShouldWork()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var todo = new Todo() { Title = "Code", Due = new(2020, 10, 3) };
      todo.AddComment("this is a comment");

      ctx.Add(todo);
      ctx.SaveChanges();

      ctx.Todos.First().Comments.First().Text.Should().Be("this is a comment");
      ctx.Comments.First().Todo.Title.Should().Be("Code");
    }

    [Fact]
    public void ShouldAddNewTag()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var todo = new Todo(ctx) { Title = "Code", Due = new(2020, 10, 3) };
      todo.AddTag("Sports", ColorEnum.Red);

      ctx.Add(todo);
      ctx.SaveChanges();

      ctx.Tags.Count().Should().Be(1);
    }

    [Fact]
    public void WhenAddingMutipleSimilarTagsShouldOnlySaveOne()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var todo = new Todo(ctx) { Title = "Code", Due = new(2020, 10, 3) };
      todo.AddTag("Running", ColorEnum.Red);
      todo.AddTag("Running", ColorEnum.Green);
      todo.AddTag("Running", ColorEnum.Blue);

      todo.Tags.Count().Should().Be(1);
    }

    [Fact]
    public void ShouldAddExistingTag()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var tag = new Tag() { Name = "Running", Color = ColorEnum.Blue };
      ctx.Add(tag);
      ctx.SaveChanges();

      var todo = new Todo(ctx) { Title = "Code", Due = new(2020, 10, 3) };
      todo.AddTag("Running", ColorEnum.Green);

      ctx.Add(todo);
      ctx.SaveChanges();

      ctx.Tags.Count().Should().Be(1);
      ctx.Todos.Count().Should().Be(1);
    }

    [Fact]
    public void NavigationBetweenTagsAndTodosShouldWork()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      var todo1 = new Todo(ctx) { Title = "Code", Due = new(2020, 10, 3) };
      todo1.AddTag("Python", ColorEnum.Blue);
      todo1.AddTag("General", ColorEnum.Red);
      ctx.Add(todo1);
      ctx.SaveChanges();

      var todo2 = new Todo(ctx) { Title = "Math", Due = new(2020, 11, 4) };
      todo2.AddTag("Linear Algebra", ColorEnum.Blue);
      todo2.AddTag("General", ColorEnum.Red);
      ctx.Add(todo2);
      ctx.SaveChanges();

      ctx.Todos.Count().Should().Be(2);
      ctx.Tags.Count().Should().Be(3);
      ctx.Todos.Where(t => t.Title == "Code").First().Tags.First().Name.Should().Be("Python");
      ctx.Tags.Where(t => t.Name == "Python").First().Todos.Where(t => t.Title == "Code").First().Title.Should().Be("Code");
    }
  }
}
