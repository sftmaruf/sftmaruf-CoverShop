using CoverShop.Application.Models;
using MediatR;

namespace CoverShop.Application.Features.Auth.Registrations.Commands;

public sealed record RegistrationCommand (
    string Name,
    string Email,
    string Password) : IRequest<IResult<RegistrationCommandResponse?>>;
