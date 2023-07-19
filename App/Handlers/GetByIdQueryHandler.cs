using App.Queries;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Course>
{
    private readonly ICourseRepository _courseRepository;

    public GetByIdQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Course> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return (_courseRepository.GetById(request.Id));
    }
}