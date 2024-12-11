namespace HighSchool.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public int Year { get; set; }

    public string Section { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}