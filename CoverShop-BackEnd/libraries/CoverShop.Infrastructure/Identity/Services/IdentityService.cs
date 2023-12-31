﻿using CoverShop.Application.Contracts;
using CoverShop.Application.Models;
using CoverShop.Infrastructure.Constants;
using CoverShop.Infrastructure.Extensions;
using CoverShop.Infrastructure.Identity.Adapters;
using CoverShop.Infrastructure.Identity.Adapters.Contracts;
using CoverShop.Infrastructure.Identity.Models;
using CoverShop.Infrastructure.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoverShop.Infrastructure.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly ISignInManagerAdapter _signInManager;
    private readonly IUserManagerAdapter _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly CustomUserClaimsPrincipalFactory _claimsPrincipalFactory;
    private readonly JwtSetting _jwtSetting;

    public IdentityService (
        IUserManagerAdapter userManager,
        ISignInManagerAdapter signInManager,
        IAuthorizationService authorizationService,
        CustomUserClaimsPrincipalFactory claimsPrincipalFactory,
        IOptions<JwtSetting> jwtSetting)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authorizationService = authorizationService;
        _claimsPrincipalFactory = claimsPrincipalFactory;
        _jwtSetting = jwtSetting.Value;
    }

    public async Task<IResult<Guid>> Register(string name, string email, string password)
    {
        var isUserExists = _userManager.Users.Any(user => user.Email!.ToLower().Equals(email.ToLower()));
       
        if (isUserExists)
        {
            return Result.Fail<Guid>("User already exists with this email address");
        };

        var user = new ApplicationUser
        {
            Name = name,
            UserName = email,
            Email = email,
            Timezone = ApplicationConstants.DefaultTimeZone
        };

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded ? Result.Success(user.Id) : result.ToResult<Guid>();
    }
    
    public async Task<IAuthResult> AuthorizeAsync(string email, string password)
    {
        var user = await _userManager.Users.SingleAsync(user => user.Email!.ToLower().Equals(email.ToLower()));
    
        if(user is null)
        {
            return AuthResult.Fail("Invalid email address"); 
        }
        
        var result = await _userManager.CheckPasswordAsync(user, password);

        if (result is false) return AuthResult.Fail("Invalid email address or password");

        return await GenerateAuthTokenAsync();
    }

    private async Task<IAuthResult> GenerateAuthTokenAsync()
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sid, "1")
        };

        var builder = new TokenBuilder()
            .AddIssuer(_jwtSetting.Issuer)
            .AddAudience(_jwtSetting.Audience)
            .AddNotBefore(DateTime.Now)
            .AddExpiry(DateTime.Now.AddMinutes(60))
            .AddClaims(claims)
            .AddKey(_jwtSetting.SecretKey)
            .Build();

        
        var token = new JwtSecurityTokenHandler().WriteToken(builder);

        return AuthResult.Success(token);
    }

    public async Task<bool> IsInClaimAsync(Guid userId, string claim, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);
        if (user is null) return false;

        var isClaimMatched = (await _userManager.GetClaimsAsync(user))
            .Any(c => string.Equals(c.Type, claim, StringComparison.InvariantCultureIgnoreCase));

        return isClaimMatched;
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);
        if (user is null) return false;

        var isRoleMatched = (await _userManager.GetRolesAsync(user))
            .Any(r => string.Equals(r, role, StringComparison.InvariantCultureIgnoreCase));

        return isRoleMatched;
    }

    public async Task<bool> AuthorizeAsync(Guid userId, string policy, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(userId, cancellationToken);
        if (user is null) return false;

        var principal = await _claimsPrincipalFactory.CreateAsync(user);
        var result = await _authorizationService.AuthorizeAsync(principal, policy);

        return result.Succeeded;
    }

    private async Task<ApplicationUser?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userManager
            .Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);

        return user;
    }
}
