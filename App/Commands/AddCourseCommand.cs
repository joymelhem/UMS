using DomainLibrary.Entities;
using DomainLibrary.Value_Objects;
using MediatR;
using NpgsqlTypes;

namespace App.Commands;

public class AddCourseCommand : IRequest<bool>
{
    public string CourseName { get; set; }
    public int MaximumStudents { get; set; }
    public string LowerBound { get; set; }
    public string UpperBound { get; set; }

}