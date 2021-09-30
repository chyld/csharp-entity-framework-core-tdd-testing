using System;
using lib;

namespace app
{
  class Program
  {
    static void Main(string[] args)
    {
      using var db = new Database();
      var tag = new Tag() { Name = "Programming", Color = ColorEnum.Green };
      var todo = new Todo() { Status = StatusEnum.Open, Priority = PriorityEnum.Low, Title = "Code", Due = new(2020, 10, 3) };
      todo.Tags.Add(tag);

      db.Add(todo);
      db.SaveChanges();
    }
  }
}
