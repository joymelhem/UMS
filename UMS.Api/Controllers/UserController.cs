using App.Commands;
using App.Queries;
using DomainLibrary.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{    
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetByIdQuery(id));
        return Ok(result);
    }
    
    [HttpPost("Add Course")]
    public async Task<IActionResult> AddCourse([FromBody] AddCourseCommand request)
    {
        var result = await _mediator.Send(request);
        if (result)
        {
            return Ok("Course created successfully.");
        }
        return BadRequest("Failed to create course.");
    }
    [HttpPost("Add Session Time")]
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