using System.Text;
using System.Text.Json;
using App.Commands;
using DomainLibrary.Entities;
using MediatR;

namespace App.Handlers;

public class SignupCommandHandler : IRequestHandler<SignupCommand, SignUpResponse>
{
    private readonly HttpClient _httpClient;
    private readonly string _firebaseBaseUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=AIzaSyBB21MNaWbzGe7icDl2cvw3SEzumLhK_B8";
    public SignupCommandHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SignUpResponse> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var signupRequest = new
            {
                email = request.Email,
                password = request.Password,
                returnSecureToken = true
            };
            var jsonRequest = JsonSerializer.Serialize(signupRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_firebaseBaseUrl, httpContent);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var signupResponse = JsonSerializer.Deserialize<SignUpResponse>(jsonResponse);
            return signupResponse;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error signing up user.");
        }
    }
}