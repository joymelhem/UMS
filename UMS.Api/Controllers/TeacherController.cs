using App.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UMS.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class TeacherController: ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Add Session Time")]
    [Authorize(Policy = "TeacherOnly")]
    public async Task<IActionResult> AddSessionTime([FromBody] AddSessionTimeCommand request)
    {
        var result = await _mediator.Send(request);
        if (result)
        {
            return Ok("Session Time created successfully.");
        }
        return BadRequest("Failed to create Session Time.");
    }
    [HttpPost("Add Teacher Per Course")]
    [Authorize(Policy = "TeacherOnly")]
    public async Task<IActionResult> AddTeacherPerCourse([FromBody] AddTeacherPerCourseCommand request)
    {
        var result = await _mediator.Send(request);
        if (result)
        {
            return Ok("Teacher Per Course registered successfully.");
        }
        return BadRequest("Failed to register Teacher Per Course.");
    }
    
    [HttpPost("Add Teacher Per Course per Session Time")]
    [Authorize(Policy = "TeacherOnly")]
    public async Task<IActionResult> AddTeacherPerCoursePerSessionTime([FromBody] AddTeacherPerCoursePerSessionTimeCommand request)
    {
        var result = await _mediator.Send(request);
        if (result)
        {
            return Ok("Teacher Per Course Per Session Time registered successfully.");
        }
        return BadRequest("Failed to register Teacher Per Course Per Session Time.");
    }
    
}