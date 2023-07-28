using App.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UMS.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class StudentController: ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Enroll in Class")]
    [Authorize(Policy = "StudentOnly")]
    public async Task<IActionResult> AddClassEnrollment([FromBody] AddClassEnrollmentCommand request)
    {
        var result = await _mediator.Send(request);
        if (result)
        {
            return Ok("Enrollment registered successfully.");
        }
        return BadRequest("Failed to register Enrollment.");
    }
}