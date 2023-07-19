using System.ComponentModel.DataAnnotations;
using NpgsqlTypes;

namespace DomainLibrary.Entities;

public partial class Course
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public int? MaxStudentsNumber { get; set; }

    public NpgsqlRange<DateOnly>? EnrolmentDateRange { get; set; }

    public virtual ICollection<TeacherPerCourse> TeacherPerCourses { get; set; } = new List<TeacherPerCourse>();

    public Course(string? name, int? maxStudentsNumber, NpgsqlRange<DateOnly>? enrolmentDateRange)
    {
        Name = name;
        MaxStudentsNumber = maxStudentsNumber;
        EnrolmentDateRange = enrolmentDateRange;
    }
}
