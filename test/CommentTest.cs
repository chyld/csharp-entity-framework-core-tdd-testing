using System;
using Xunit;
using FluentAssertions;
using lib;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace test
{
  public class CommentTest
  {
    private DbContextOptions<Database> _options;

    public CommentTest()
    {
      var conn = new SqliteConnection("DataSource=:memory:");
      conn.Open();

      _options = new DbContextOptionsBuilder<Database>()
         .UseSqlite(conn)
         .Options;
    }

    [Fact]
    public void ShouldCreateBasicComment()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      ctx.Add(new Comment() { Text = "This is a comment" });
      ctx.SaveChanges();

      ctx.Comments.Count().Should().Be(1);
    }
  }
}
