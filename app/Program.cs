using System;
using lib;

namespace app
{
  class Program
  {
    static void Main(string[] args)
    {
      using var db = new Database();
      var t = new Tag() { Name = "Programming", Color = ColorEnum.Green };
      db.Add(t);
      db.SaveChanges();
    }
  }
}
