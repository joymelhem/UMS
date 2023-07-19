using App.Commands;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class AddTeacherPerCoursePerSessionTimeCommandHandler : IRequestHandler<AddTeacherPerCoursePerSessionTimeCommand, bool>
{
    private readonly ITeacherPerCoursePerSessionTimeRepository _teacherPerCoursePerSessionTimeRepository;
    private readonly ITeacherPerCourseRepository _teacherPerCourseRepository;
    private readonly ISessionTimeRepository _sessionTimeRepository;

    public AddTeacherPerCoursePerSessionTimeCommandHandler(ITeacherPerCoursePerSessionTimeRepository teacherPerCoursePerSessionTimeRepository,
        ITeacherPerCourseRepository teacherPerCourseRepository,
        ISessionTimeRepository sessionTimeRepository)
    {
        _teacherPerCoursePerSessionTimeRepository = teacherPerCoursePerSessionTimeRepository;
        _teacherPerCourseRepository = teacherPerCourseRepository;
        _sessionTimeRepository = sessionTimeRepository;
    }

    public async Task<bool> Handle(AddTeacherPerCoursePerSessionTimeCommand request, CancellationToken cancellationToken)
    {
        var teacherPerCourse = await _teacherPerCourseRepository.GetById(request.TeacherPerCourseId);
        var sessionTime = await _sessionTimeRepository.GetById(request.SessionTimeId);

        if (teacherPerCourse != null && sessionTime != null)
        {
            var teacherPerCoursePerSessionTime = new TeacherPerCoursePerSessionTime
            {
                TeacherPerCourseId = teacherPerCourse.Id,
                SessionTimeId = sessionTime.Id
            };

            _teacherPerCoursePerSessionTimeRepository.Add(teacherPerCoursePerSessionTime);
            await _teacherPerCoursePerSessionTimeRepository.SaveChangesAsync();

            return true; 
        }
        return false; 
    }
}