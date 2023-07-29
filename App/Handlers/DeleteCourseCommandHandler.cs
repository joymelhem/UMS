using App.Commands;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly ICourseRepository _courseRepository;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = _courseRepository.GetById(request.CourseId);
        if (course == null)
        {
            return false;
        }
        _courseRepository.Delete(course);
        await _courseRepository.SaveChangesAsync();
        return true;
    }
}