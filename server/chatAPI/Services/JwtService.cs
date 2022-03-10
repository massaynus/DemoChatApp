using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using chatAPI.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace chatAPI.Services;

/// <summary>
/// Everything the app needs to do with JWT tokens
/// </summary>
public class JwtService
{
    const string PPID_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier";

    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Generate a JWT token from UserData
    /// </summary>
    /// <param name="user">the users informations</param>
    /// <returns >a string containing the generated JWT token</returns>
    public string GenerateToken(UserData user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(PPID_CLAIM, user.ID.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Audience"],
          claims,
          expires: DateTime.Now.AddMinutes(60 * 8),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Extract the PPID claim from a list of claims
    /// </summary>
    /// <param name="claims">the claims list</param>
    /// <returns>the extracted PPID or an empty GUID</returns>
    public Guid GetPPID(IEnumerable<Claim> claims)
    {
        var claim = claims.FirstOrDefault(c => c.Type == PPID_CLAIM);

        if (claim is null)  return Guid.Empty;

        return Guid.Parse(claim.Value);
    }
}