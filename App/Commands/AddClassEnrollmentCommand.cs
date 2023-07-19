using MediatR;

namespace App.Commands;

public class AddClassEnrollmentCommand: IRequest<bool>
{ 
    public long CourseId { get; set; }
    public long StudentId { get; set; }
}