using MediatR;
using Microsoft.AspNetCore.Mvc;
using CoverShop.Application.Features.Auth.Registrations.Commands;
using CoverShop.Application.Extensions;
using CoverShop.Api.Extensions;
using CoverShop.Application.Features.Auth.Login.Commands;
using Microsoft.AspNetCore.Authorization;
using CoverShop.Application.Features.Temp;

namespace CoverShop.Api.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class AuthsController : ControllerBase
{
    private readonly ISender _sender;

    public AuthsController(ISender Sender)
    {
        _sender = Sender;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationCommand command)
    {
        return await command.ExecuteAsync(c => _sender.Send(c))
                            .Match(Ok, BadRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        return await command.ExecuteAsync(c => _sender.Send(c))
                            .Match(Ok, BadRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Temp(TempCommand command)
    {
        await command.ExecuteAsync(c => _sender.Send(c));
        return Ok();
    }
}
