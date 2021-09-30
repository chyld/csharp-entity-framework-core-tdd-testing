using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lib
{
  public class Todo
  {
    private Database _db;
    public int Id { get; set; }
    public StatusEnum Status { get; set; }
    public PriorityEnum Priority { get; set; }
    public string Title { get; set; }
    public DateTime Due { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Comment> Comments { get; set; }

    public Todo(Database db = null)
    {
      _db = db;
      Status = StatusEnum.Open;
      Priority = PriorityEnum.Low;
      Tags = new();
      Comments = new();
    }

    public void Complete()
    {
      Status = StatusEnum.Closed;
    }

    public void AddComment(string text)
    {
      var comment = new Comment() { Text = text };
      Comments.Add(comment);
    }

    public void AddTag(string name, ColorEnum color)
    {
      if (Tags.Where(t => t.Name == name).Count() is 1) return;

      var tag = _db.Tags.Where(t => t.Name == name).FirstOrDefault();
      if (tag is null)
        tag = new Tag() { Name = name, Color = color };
      Tags.Add(tag);
    }
  }
}
