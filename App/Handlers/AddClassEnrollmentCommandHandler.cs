using App.Commands;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;
using Persistence.Services;

namespace App.Handlers;

public class AddClassEnrollmentCommandHandler : IRequestHandler<AddClassEnrollmentCommand, bool>
{
    private readonly IClassEnrollmentRepository _classEnrollmentRepository;
    private readonly ITeacherPerCourseRepository _teacherPerCourseRepository;
    private readonly IUserRepository _userRepository;
    private readonly FirebaseMailService _mailService;
    
    public AddClassEnrollmentCommandHandler(FirebaseMailService mailService,IClassEnrollmentRepository classEnrollmentRepository,
        ITeacherPerCourseRepository teacherPerCourseRepository,
        IUserRepository userRepository)
    {
        _classEnrollmentRepository = classEnrollmentRepository;
        _teacherPerCourseRepository = teacherPerCourseRepository;
        _userRepository = userRepository;
        _mailService = mailService;
    }

    public async Task<bool> Handle(AddClassEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var course = await _teacherPerCourseRepository.GetByCourseId(request.CourseId);
        var student = await _userRepository.GetById(request.StudentId);
        if (course == null || student == null)
        {
            return false;
        }

        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        if (dateNow >= course.EnrolmentDateRange.Value.LowerBound &&
            dateNow <= course.EnrolmentDateRange.Value.UpperBound)
        {
            var classEnrollment = new ClassEnrollment
            {
                ClassId = course.Id,
                StudentId = student.Id
            };
            _classEnrollmentRepository.Add(classEnrollment);
            await _classEnrollmentRepository.SaveChangesAsync();
            var subject = "Class Enrollment Confirmation";
            var body = $"Dear {student.Name}, you have successfully enrolled in {course.Name}.";
            await _mailService.SendMail(student.Email, subject, body);
            return true;
        }

        return false;
    }
}