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
    public async Task<IActionResult> CreateCourse([FromBody] AddCourseCommand request)
    {
        var result = await _mediator.Send(request);
        if (result)
        {
            return Ok("Course created successfully.");
        }
        else
        {
            return BadRequest("Failed to create course.");
        }
    }
}