using CoverShop.Application.Models;
using Microsoft.AspNetCore.Mvc;

using IResult = CoverShop.Application.Models.IResult;

namespace CoverShop.Api.Extensions;

public static class ResultExtensions
{
    public static async Task<IActionResult> Match<TOut>(
        this Task<IResult<TOut>> resultTask,
        Func<IResult<TOut>, IActionResult> success,
        Func<IResult<TOut>, IActionResult> fail)
    {
        var result = await resultTask;
        return result.IsSuccess ? success(result) : fail(result);
    }

    public static async Task<IActionResult> Match(
    this Task<IResult> resultTask,
    Func<IResult, IActionResult> success,
    Func<IResult, IActionResult> fail)
    {
        var result = await resultTask;
        return result.IsSuccess ? success(result) : fail(result);
    }
}
