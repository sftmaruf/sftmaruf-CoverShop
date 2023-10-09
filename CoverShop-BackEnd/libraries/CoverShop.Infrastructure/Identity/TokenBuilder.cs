using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoverShop.Infrastructure.Identity;

public class TokenBuilder
{
    private string? _issuer;
    private string? _audience;
    private IEnumerable<Claim>? _claims;
    private DateTime _notBefore;
    private DateTime _expires;
    private SymmetricSecurityKey _key = null!;
    private SigningCredentials _signingCredentials = null!;
    private string _algorithm = SecurityAlgorithms.HmacSha512Signature;

    public TokenBuilder AddIssuer(string issuer)
    {
        _issuer = issuer;
        return this;
    }

    public TokenBuilder AddAudience(string audience)
    {
        _audience = audience;
        return this;
    }

    public TokenBuilder AddClaims(IEnumerable<Claim> claims)
    {
        _claims = claims;
        return this;
    }

    public TokenBuilder AddNotBefore(DateTime notBefore)
    {
        _notBefore = notBefore;
        return this;
    }

    public TokenBuilder AddExpiry(DateTime expires)
    {
        _expires = expires;
        return this;
    }

    public TokenBuilder AddKey(string key)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        _signingCredentials = new SigningCredentials(_key, _algorithm);
        return this;
    }

    public TokenBuilder AddAlgorithm(string algorithm)
    {
        _algorithm = algorithm;
        return this;
    }

    public JwtSecurityToken Build()
    {
        return new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: _claims,
            notBefore: _notBefore,
            expires: _expires,
            signingCredentials: _signingCredentials);
    }
}
