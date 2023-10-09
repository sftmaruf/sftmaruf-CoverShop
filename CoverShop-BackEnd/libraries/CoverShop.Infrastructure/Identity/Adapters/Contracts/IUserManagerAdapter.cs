using CoverShop.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoverShop.Infrastructure.Identity.Adapters.Contracts;

public interface IUserManagerAdapter
{
    IQueryable<ApplicationUser> Users { get; }
    Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
    Task<string> GetUserIdAsync(ApplicationUser user);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
    Task<IList<string>> GetRolesAsync(ApplicationUser user);
}
