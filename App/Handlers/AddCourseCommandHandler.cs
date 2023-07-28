using App.Commands;
using App.Queries;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;
using NpgsqlTypes;

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
        var lowerBound = DateOnly.Parse(request.LowerBound);
        var upperBound = DateOnly.Parse(request.UpperBound);

        var enrollmentDateRange = new NpgsqlRange<DateOnly>(lowerBound, upperBound);

        var course = new Course(request.CourseName, request.MaximumStudents, enrollmentDateRange);

        _courseRepository.Add(course);
        await _courseRepository.SaveChangesAsync();
        return true;
    }
}
