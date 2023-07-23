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
                // Create the login request object
                var loginRequest = new
                {
                    email = request.email,
                    password = request.password,
                    returnSecureToken = true
                };

                // Serialize the request object
                var jsonRequest = JsonSerializer.Serialize(loginRequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Make the POST request to Firebase Authentication REST API
                var response = await _httpClient.PostAsync(_firebaseBaseUrl, httpContent);
                response.EnsureSuccessStatusCode();

                // Deserialize the response to get the JWT token
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(jsonResponse);

                return loginResponse;
            }
            catch (HttpRequestException ex)
            {
                // Failed to authenticate user or other errors
                throw new Exception("Error signing in user.");
            }
        }
    }
}