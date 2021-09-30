using System;
using System.Collections.Generic;

namespace lib
{
  public class Todo
  {
    public int Id { get; set; }
    public StatusEnum Status { get; set; }
    public PriorityEnum Priority { get; set; }
    public string Title { get; set; }
    public DateTime Due { get; set; }
    public List<Tag> Tags { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();

    public void Completed()
    {
      Status = StatusEnum.Closed;
    }
  }
}
