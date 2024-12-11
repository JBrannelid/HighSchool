namespace HighSchool.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}