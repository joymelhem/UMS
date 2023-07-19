using App.Commands;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class AddTeacherPerCourseCommandHandler : IRequestHandler<AddTeacherPerCourseCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ITeacherPerCourseRepository _teacherPerCourseRepository;

    public AddTeacherPerCourseCommandHandler(IUserRepository userRepository, ICourseRepository courseRepository, ITeacherPerCourseRepository teacherPerCourseRepository)
    {
        _userRepository = userRepository;
        _courseRepository = courseRepository;
        _teacherPerCourseRepository = teacherPerCourseRepository;
    }

    public async Task<bool> Handle(AddTeacherPerCourseCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _userRepository.GetById(request.TeacherId);
        var course =  _courseRepository.GetById(request.CourseId);
        if (teacher != null && course != null)
        {
            var teacherPerCourse = new TeacherPerCourse
            {
                TeacherId = teacher.Id,
                CourseId = course.Id
            };
            _teacherPerCourseRepository.Add(teacherPerCourse);
            await _teacherPerCourseRepository.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
}