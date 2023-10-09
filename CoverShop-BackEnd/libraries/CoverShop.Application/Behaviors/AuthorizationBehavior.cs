using CoverShop.Application.Attributes;
using CoverShop.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace CoverShop.Application.Behaviors;

internal class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly HttpContext _httpContext;
    private readonly IIdentityService _identityService;

    public AuthorizationBehavior(IIdentityService identityService, IHttpContextAccessor contextAccessor)
    {
        _identityService = identityService;
        _httpContext = contextAccessor.HttpContext;    
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request!.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();
        var userId = Guid.TryParse(_httpContext.User.FindFirst(JwtRegisteredClaimNames.Sid)?.Value, out var id) ? id : Guid.Empty;

        if(authorizeAttributes.Any())
        {
            if (userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var authorizeAttributesWithClaims = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Claims)).ToList();
            if(authorizeAttributesWithClaims.Any())
            {
                var authorized = await ValidateClaimsAsync(authorizeAttributes, userId, cancellationToken);
                if (!authorized) throw new Exception();
            }

            var authorizeAttributesWithrRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles)).ToList();
            if (authorizeAttributesWithrRoles.Any())
            {
                var authorized = await ValidateRolesAsync(authorizeAttributesWithrRoles, userId, cancellationToken);
                if (!authorized) throw new Exception();
            }

            var authorizeAttributesWithPolicy = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy)).ToList();
            if (authorizeAttributesWithPolicy.Any())
            {
                var authorized = await ValidatePoliciesAsync(authorizeAttributesWithPolicy, userId, cancellationToken);
                if (!authorized) throw new Exception();
            }
        }

        var response = await next();
        return response;
    }

    private async Task<bool> ValidateClaimsAsync(List<AuthorizeAttribute> authorizeAttributesWithClaims, Guid userId, CancellationToken cancellationToken)
    {
        foreach (var claims in authorizeAttributesWithClaims.Select(a => a.Claims.Split(',')))
        {
            foreach (var claim in claims)
            {
                var isInClaim = await _identityService.IsInClaimAsync(userId, claim, cancellationToken);
                if (isInClaim)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private async Task<bool> ValidateRolesAsync(List<AuthorizeAttribute> authorizeAttributesWithRoles, Guid userId, CancellationToken cancellationToken)
    {
        foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
        {
            foreach (var role in roles)
            {
                var isInRole = await _identityService.IsInClaimAsync(userId, role, cancellationToken);
                if (isInRole)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private async Task<bool> ValidatePoliciesAsync(List<AuthorizeAttribute> authorizeAttributesWithPolicies, Guid userId, CancellationToken cancellationToken)
    {
        foreach (var policies in authorizeAttributesWithPolicies.Select(a => a.Policy.Split(',')))
        {
            foreach (var policy in policies)
            {
                var isInPolicy = await _identityService.AuthorizeAsync(userId, policy, cancellationToken);
                if (isInPolicy) return true;
            }
        }

        return false;
    }
}
