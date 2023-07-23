using DomainLibrary.Entities;
using MediatR;

namespace App.Commands;

public class SignupCommand : IRequest<SignUpResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}