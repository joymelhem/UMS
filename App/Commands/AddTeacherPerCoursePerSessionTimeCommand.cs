using MediatR;

namespace App.Commands;

public class AddTeacherPerCoursePerSessionTimeCommand : IRequest<bool>
{
    public long TeacherPerCourseId { get; set; }
    public long SessionTimeId { get; set; }
}