using App.Queries;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class GetGenderDistributionByCourseQueryHandler : IRequestHandler<GetGenderDistributionByCourseQuery, List<GenderDistributionDto>>
{
    private readonly ITeacherPerCourseRepository _teacherPerCourseRepository;

    public GetGenderDistributionByCourseQueryHandler(ITeacherPerCourseRepository teacherPerCourseRepository)
    {
        _teacherPerCourseRepository = teacherPerCourseRepository;
    }


    public async Task<List<GenderDistributionDto>> Handle(GetGenderDistributionByCourseQuery request, CancellationToken cancellationToken)
    {
        return await _teacherPerCourseRepository.GetGenderDistributionByCourse();;
    }
}