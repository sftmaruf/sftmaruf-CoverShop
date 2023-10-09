using CoverShop.Application.Models;
using MediatR;

namespace CoverShop.Application.Features.Auth.Login.Commands;

public record LoginCommand(string Email, string Password) : IRequest<IResult>;
