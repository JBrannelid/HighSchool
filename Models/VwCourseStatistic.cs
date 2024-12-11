namespace HighSchool.Models;

public partial class VwCourseStatistic
{
    public string CourseName { get; set; } = null!;

    public int? NumberOfGrades { get; set; }

    public double? AverageGrade { get; set; }

    public string? HighestGrade { get; set; }

    public string? LowestGrade { get; set; }
}