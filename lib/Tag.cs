using System.ComponentModel.DataAnnotations;

namespace lib
{
  public class Tag
  {
    [Key]
    public string Name { get; set; }
    public ColorEnum Color { get; set; }
    public Todo Todo { get; set; }
  }
}
