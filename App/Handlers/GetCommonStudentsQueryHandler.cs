using App.Queries;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;



public class GetCommonStudentsQueryHandler : IRequestHandler<GetCommonStudentsQuery, List<string>>
{
    private readonly ITeacherPerCourseRepository _teacherPerCourseRepository;

    public GetCommonStudentsQueryHandler(ITeacherPerCourseRepository teacherPerCourseRepository)
    {
        _teacherPerCourseRepository = teacherPerCourseRepository;
    }

    public async Task<List<string>> Handle(GetCommonStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _teacherPerCourseRepository.GetCommonStudents(request.TeacherId1, request.TeacherId2);
    }
}
