using CoverShop.Application.Contracts;
using CoverShop.Application.Models;
using MediatR;

namespace CoverShop.Application.Features.Auth.Login.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, IResult>
{
    private readonly IIdentityService _identityService;
    
    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<IResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.AuthorizeAsync(request.Email, request.Password);
    }
}
