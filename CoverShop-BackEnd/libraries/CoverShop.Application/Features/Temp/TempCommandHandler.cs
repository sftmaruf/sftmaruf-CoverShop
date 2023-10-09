using MediatR;

namespace CoverShop.Application.Features.Temp;

internal class TempCommandHandler : IRequestHandler<TempCommand>
{
    public Task Handle(TempCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Ok");
        
        return Task.CompletedTask;
    }
}
