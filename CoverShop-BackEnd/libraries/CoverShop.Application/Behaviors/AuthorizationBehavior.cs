using CoverShop.Application.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace CoverShop.Application.Behaviors;

internal class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly HttpContext _httpContext;

    public AuthorizationBehavior(IHttpContextAccessor contextAccessor)
    {
        _httpContext = contextAccessor.HttpContext;    
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request!.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();
        var userId = Guid.TryParse(_httpContext.User.FindFirst(JwtRegisteredClaimNames.Sid)?.Value, out var id);

        if(authorizeAttributes.Any())
        {
            var authorized = false;

            var authorizeAttributesWithClaims = authorizeAttributes.Where(a => !string.IsNullOrEmpty(a.Claims)).ToList();
            if(authorizeAttributesWithClaims.Any())
            {
                foreach(var claims in authorizeAttributesWithClaims.Select(a => a.Claims.Split(',')))
                {
                    foreach(var claim in claims)
                    {
                        var isInClaim = true;// if user in claim
                        if(isInClaim)
                        {
                            authorized = true;
                        }
                    }
                }
            }


            if (!authorized)
            {
                throw new Exception();
            }
        }

        return next();
    }
}
