using CoverShop.Application.Models;

namespace CoverShop.Application.Contracts;

public interface IIdentityService
{
    Task<IResult<Guid>> Register(string name, string email, string password);
    Task<IAuthResult> AuthorizeAsync(string email, string password);
}
