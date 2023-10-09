using CoverShop.Application.Contracts;
using CoverShop.Application.Models;
using MediatR;

namespace CoverShop.Application.Features.Auth.Registrations.Commands;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, IResult<RegistrationCommandResponse?>>
{
    private readonly IIdentityService _identityService;

    public RegistrationCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<IResult<RegistrationCommandResponse?>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.Register(request.Name, request.Email, request.Password);
        
        if(result.IsSuccess)
        {
            return Result.Success(new RegistrationCommandResponse(UserId: result.Data));
        }

        return Result.Fail<RegistrationCommandResponse?>("User registration failed");
    }
}
