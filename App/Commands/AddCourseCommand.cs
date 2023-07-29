using MediatR;

namespace App.Commands;

public class AddCourseCommand : IRequest<bool>
{
    public string CourseName { get; set; }
    public int MaximumStudents { get; set; }
    public string LowerBound { get; set; }
    public string UpperBound { get; set; }

}