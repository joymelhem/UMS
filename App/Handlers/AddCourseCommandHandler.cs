using App.Commands;
using App.Queries;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, bool>
{
    private readonly ICourseRepository _courseRepository;

    public AddCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<bool> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course(request.CourseName, request.MaximumStudents, request.EnrollmentDateRange);
        _courseRepository.Add(course);
        await _courseRepository.SaveChangesAsync();
        return true; 
    }
}