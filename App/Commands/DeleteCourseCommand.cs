using MediatR;

namespace App.Commands;

public class DeleteCourseCommand : IRequest<bool>
{
    public long CourseId { get; set; }
}