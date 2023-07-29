using App.Commands;
using App.Queries;
using DomainLibrary.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UMS.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class AdminController: ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Add Course")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> AddCourse([FromBody] AddCourseCommand request)
    {
        var result = await _mediator.Send(request);
        if (result)
        {
            return Ok("Course created successfully.");
        }
        return BadRequest("Failed to create course.");
    }
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteCourseCommand { CourseId = id });
        if (result)
        {
            return Ok("Course deleted.");
        }
        return NotFound("Course not found.");
    }
    [HttpGet("Get Gender Distribution By Course")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<List<GenderDistributionDto>>> GenderDistribution()
    {
        var genderDistribution = await _mediator.Send(new GetGenderDistributionByCourseQuery());
        return Ok(genderDistribution);
    }
    [HttpGet("Get Common Students")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<List<string>>> GetCommonStudents([FromQuery] long teacherId1, [FromQuery] long teacherId2)
    {
        var commonStudents = await _mediator.Send(new GetCommonStudentsQuery(teacherId1, teacherId2));
        return Ok(commonStudents);
    }
}