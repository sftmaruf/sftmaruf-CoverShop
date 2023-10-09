using CoverShop.Application.Models;
using MediatR;

namespace CoverShop.Application.Extensions;

public static class RequestExtensions
{
    public static async Task<IResult<TOut>> ExecuteAsync<TCommand, TOut>(this TCommand request,
        Func<TCommand, Task<IResult<TOut>>> func) where TCommand : IBaseRequest
    {
        return await func(request);
    }

    public static async Task<IResult> ExecuteAsync<TCommand>(this TCommand request,
        Func<TCommand, Task<IResult>> func) where TCommand : IBaseRequest
    {
        return await func(request);
    }

    public static async Task ExecuteAsync<TCommand>(this TCommand request,
        Func<TCommand, Task> func) where TCommand : IBaseRequest
    {
        await func(request);
    }
}
