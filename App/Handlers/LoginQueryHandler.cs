using System.Text;
using System.Text.Json;
using App.Commands;
using DomainLibrary.Entities;
using MediatR;

namespace App.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly string _firebaseBaseUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyBB21MNaWbzGe7icDl2cvw3SEzumLhK_B8";
        public LoginQueryHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loginRequest = new
                {
                    email = request.email,
                    password = request.password,
                    returnSecureToken = true
                };

                var jsonRequest = JsonSerializer.Serialize(loginRequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_firebaseBaseUrl, httpContent);
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(jsonResponse);
                return loginResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Invalid email/password.");
            }
        }
    }
}