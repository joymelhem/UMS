using System;
using System.Net.Http;
using System.Threading.Tasks;
using App.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        try
        {
            string baseUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=AIzaSyBB21MNaWbzGe7icDl2cvw3SEzumLhK_B8";
            var signUpRequest = new
            {
                email = model.Email,
                password = model.Password,
                returnSecureToken = true
            };
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(baseUrl, signUpRequest);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadFromJsonAsync<SignUpResponse>();
            var jwtToken = responseBody.idToken;

            return Ok(new { Token = jwtToken });
        }
        catch (HttpRequestException ex)
        {
            return BadRequest("Error signing up user.");
        }
    }
}



public class SignUpRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class SignUpResponse
{
    public string idToken { get; set; }
}