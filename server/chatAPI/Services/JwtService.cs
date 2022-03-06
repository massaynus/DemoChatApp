using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using chatAPI.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace chatAPI.Services;

public class JwtService
{
    const string PPID_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier";

    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(PPID_CLAIM, user.ID.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirht.ToShortDateString()),
            };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Audience"],
          claims,
          expires: DateTime.Now.AddMinutes(60 * 8),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}