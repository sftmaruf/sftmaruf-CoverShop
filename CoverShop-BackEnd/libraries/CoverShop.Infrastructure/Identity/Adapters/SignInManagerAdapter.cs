using CoverShop.Infrastructure.Identity.Adapters.Contracts;

namespace CoverShop.Infrastructure.Identity.Adapters;

public class SignInManagerAdapter : ISignInManagerAdapter
{
    private readonly ApplicationSignInManager _signInManager;

    public SignInManagerAdapter(ApplicationSignInManager signInManager)
    {
         _signInManager = signInManager;
    }

}
