using MediatR;

namespace App.Commands;

public class AddTeacherPerCourseCommand : IRequest<bool>
{
    public long TeacherId { get; set; }
    public long CourseId { get; set; }
}