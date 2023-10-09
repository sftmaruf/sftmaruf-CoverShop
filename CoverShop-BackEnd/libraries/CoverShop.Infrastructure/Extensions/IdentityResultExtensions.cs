using CoverShop.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using IResult = CoverShop.Application.Models.IResult;

namespace CoverShop.Infrastructure.Extensions;

public static class IdentityResultExtensions
{
    public static IResult ToResult(this IdentityResult result)
    {
        return result.Succeeded 
            ? Result.Success()
            : Result.Fail(StatusCodes.Status400BadRequest,
                result.Errors.Select(error => error.Description).ToArray());
    }

    public static IResult<T?> ToResult<T>(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success<T>()
            : Result.Fail<T>(StatusCodes.Status400BadRequest,
                result.Errors.Select(error => error.Description).ToArray());
    }
}
