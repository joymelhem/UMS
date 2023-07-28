using App.Commands;
using DomainLibrary.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var query = new LoginQuery
        {
            email = email,
            password = password
        };
        var response = await _mediator.Send(query);
        return Ok(new { Token = response.idToken });
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest model)
    {
        var command = new SignupCommand
        {
            Email = model.Email,
            Password = model.Password
        };
        var response = await _mediator.Send(command);
        return Ok(new { Token = response.idToken });
    }
}