using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using App.Commands;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace App.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginQueryHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecretKey = "wVeMNEEqCZVo7OZdByT8eIO34PvEqfXyHWJe3lVWusg=";
        private readonly string _firebaseBaseUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyBB21MNaWbzGe7icDl2cvw3SEzumLhK_B8";
        public LoginQueryHandler(IUserRepository userRepository,HttpClient httpClient, ILogger<LoginQueryHandler> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _userRepository = userRepository;
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

                if (!string.IsNullOrEmpty(loginResponse?.idToken))
                {
                    var user = await _userRepository.GetByEmail(request.email);
                    if (user != null)
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                            new Claim("email", user.Email) ,
                            new Claim("branchid", user.branchid.ToString())
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.UtcNow.AddHours(1), 
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        loginResponse.idToken = tokenHandler.WriteToken(token);
                    }
                }
                return loginResponse;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Invalid email/password.");
                throw new Exception("Invalid email/password.");
            }
        }
    }
}
