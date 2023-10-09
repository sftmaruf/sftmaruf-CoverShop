using CoverShop.Application.Models;

namespace CoverShop.Application.Contracts;

public interface IIdentityService
{
    Task<IResult<Guid>> Register(string name, string email, string password);
    Task<IAuthResult> AuthorizeAsync(string email, string password);
    Task<bool> IsInClaimAsync(Guid id, string claim, CancellationToken cancellationToken);
    Task<bool> IsInRoleAsync(Guid id, string role, CancellationToken cancellationToken);
    Task<bool> AuthorizeAsync(Guid userId, string policy, CancellationToken cancellationToken);
}
