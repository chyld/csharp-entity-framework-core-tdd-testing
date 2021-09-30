using System;
using Xunit;
using FluentAssertions;
using lib;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace test
{
  public class TagTest
  {
    private DbContextOptions<Database> _options;

    public TagTest()
    {
      var conn = new SqliteConnection("DataSource=:memory:");
      conn.Open();

      _options = new DbContextOptionsBuilder<Database>()
         .UseSqlite(conn)
         .Options;
    }

    [Fact]
    public void ShouldCreateBasicTag()
    {
      using var ctx = new Database(_options);
      ctx.Database.EnsureDeleted();
      ctx.Database.EnsureCreated();

      ctx.Add(new Tag() { Name = "Home", Color = ColorEnum.Blue });
      ctx.SaveChanges();

      ctx.Tags.Count().Should().Be(1);
    }
  }
}
