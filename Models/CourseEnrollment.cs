namespace HighSchool.Models;

public partial class CourseEnrollment
{
    public int EnrollmentId { get; set; }

    public string? Grade { get; set; }

    public DateTime? GradeAssignedDate { get; set; }

    public int FkstudentId { get; set; }

    public int FkcourseId { get; set; }

    public int? FkteacherId { get; set; }

    public virtual Course Fkcourse { get; set; } = null!;

    public virtual Person Fkstudent { get; set; } = null!;

    public virtual Person? Fkteacher { get; set; }

    public virtual GradeValue? GradeNavigation { get; set; }
}