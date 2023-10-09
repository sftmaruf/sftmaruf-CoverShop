using CoverShop.Infrastructure.Identity.Adapters.Contracts;
using CoverShop.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoverShop.Infrastructure.Identity.Adapters;

public class UserManagerAdapter : IUserManagerAdapter
{
    private readonly ApplicationUserManager _userManager;

    public UserManagerAdapter(ApplicationUserManager userManager)
    {
        _userManager = userManager;
    }

    public IQueryable<ApplicationUser> Users => _userManager.Users;

    public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }

    public Task<string> GetUserIdAsync(ApplicationUser user) 
    {
        return _userManager.GetUserIdAsync(user);
    }

    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        return _userManager.CheckPasswordAsync(user, password);
    }

    public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
    {
        return _userManager.GetClaimsAsync(user);
    }

    public Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return _userManager.GetRolesAsync(user);
    }
}
