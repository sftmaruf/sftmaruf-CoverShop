using CoverShop.Application.Attributes;
using MediatR;

namespace CoverShop.Application.Features.Temp;

[Authorize]
public record TempCommand() : IRequest;
