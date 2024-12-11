namespace HighSchool.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Pin { get; set; } = null!;

    public bool Role { get; set; }

    public int? FkclassId { get; set; }

    public int? FkpositionId { get; set; }

    public virtual ICollection<CourseEnrollment> CourseEnrollmentFkstudents { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<CourseEnrollment> CourseEnrollmentFkteachers { get; set; } = new List<CourseEnrollment>();

    public virtual Class? Fkclass { get; set; }

    public virtual Position? Fkposition { get; set; }
}