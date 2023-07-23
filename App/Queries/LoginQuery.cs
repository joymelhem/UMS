using DomainLibrary.Entities;
using MediatR;

namespace App.Commands;

public class LoginQuery : IRequest<LoginResponse>
{
    public string email {get; set;}
    public string password {get; set;}
}