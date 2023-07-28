using App.Commands;
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
    
}