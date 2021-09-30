using System;
using lib;

namespace app
{
  class Program
  {
    static void Main(string[] args)
    {
      using var db = new Database();

      var todo = new Todo(db) { Title = "Code", Due = new(2020, 10, 3) };
      todo.AddTag("C#", ColorEnum.Blue);
      todo.AddTag(".NET", ColorEnum.Green);
      todo.AddTag("EF Core", ColorEnum.Blue);
      todo.AddComment("Work on Project");

      db.Add(todo);
      db.SaveChanges();
    }
  }
}
