using App.Commands;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class AddClassEnrollmentCommandHandler : IRequestHandler<AddClassEnrollmentCommand, bool>
{
    private readonly IClassEnrollmentRepository _classEnrollmentRepository;
    private readonly ITeacherPerCourseRepository _teacherPerCourseRepository;
    private readonly IUserRepository _userRepository;

    public AddClassEnrollmentCommandHandler(IClassEnrollmentRepository classEnrollmentRepository,
            ITeacherPerCourseRepository teacherPerCourseRepository,
            IUserRepository userRepository)
    {
        _classEnrollmentRepository = classEnrollmentRepository;
        _teacherPerCourseRepository = teacherPerCourseRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(AddClassEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var teacherPerCourse = await _teacherPerCourseRepository.GetByCourseId(request.CourseId);
        var student = await _userRepository.GetById(request.StudentId);
        if (teacherPerCourse == null || student == null)
        {
            return false;
        }
        var dateNow = DateOnly.FromDateTime(DateTime.Now); 
        if (dateNow >= teacherPerCourse.EnrolmentDateRange.Value.LowerBound &&
                                                               dateNow <= teacherPerCourse.EnrolmentDateRange.Value.UpperBound)
        {
            var classEnrollment = new ClassEnrollment
            {
                ClassId = teacherPerCourse.Id,
                StudentId = student.Id
            };
            _classEnrollmentRepository.Add(classEnrollment);
            await _classEnrollmentRepository.SaveChangesAsync();
            return true; 
        }

        return false; 
    }
}