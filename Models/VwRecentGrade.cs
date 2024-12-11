namespace HighSchool.Models;

public partial class VwRecentGrade
{
    public string StudentName { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public string? Grade { get; set; }

    public int GradeValue { get; set; }

    public DateTime? GradeAssignedDate { get; set; }

    public string TeacherName { get; set; } = null!;
}