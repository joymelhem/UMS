using App.Queries;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, List<Course>>
{
    private readonly ICourseRepository _courseRepository;

    public GetAllCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public Task<List<Course>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        return (_courseRepository.GetAll());
    }
}